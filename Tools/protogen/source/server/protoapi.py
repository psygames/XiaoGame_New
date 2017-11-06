from flask import Flask, send_file
from flask import request
import os
import subprocess
import zipfile
import uuid
import shutil
from gevent.pywsgi import WSGIServer
import platform

app = Flask(__name__)
app.config.update(DEBUG=True)
just_test = False
plugin_name = 'plugins3'


@app.route('/proto/gen', methods=['POST'])
def proto_gen():
    orig_plugin_dir = plugin_name
    plugin_dir = 'cache/plugins_%s' % uuid.uuid1()
    shutil.copytree(orig_plugin_dir, plugin_dir)
    stype = request.args['type']
    out_type = request.args['out_type']
    filename = request.args['filename']

    save_dir = '%s/%s.proto' % (plugin_dir, filename)
    zip_save_dir = '%s/%s.zip' % (plugin_dir, filename)
    ret = None
    if stype == 'file':
        for f in request.files:
            request.files[f].save(save_dir)
            break
    elif stype == 'text':
        text = request.form['text']
        if text is not None:
            f = open(save_dir, 'w')
            f.write(text)
            f.close()
    elif stype == 'zip':
        for f in request.files:
            request.files[f].save(zip_save_dir)
            f = zipfile.ZipFile(zip_save_dir, 'r')
            for file in f.namelist():
                f.extract(file, "%s/" % plugin_dir)
                if file.endswith('.proto'):
                    shutil.move(plugin_dir + os.sep + file, plugin_dir + os.sep + os.path.basename(file))
            break
    else:
        ret = 'type error'

    run_plugin(plugin_dir)

    if ret is not None:
        pass
    elif out_type == 'file':
        out_name = os.path.splitext(filename)[0] + '.cs'
        ret = send_file('%s/%s.cs' % (plugin_dir, filename), as_attachment=True, attachment_filename=out_name)
    elif out_type == 'text':
        ret = read_text('%s/%s.cs' % (plugin_dir, filename))
    elif out_type == 'zip':
        zip_file_name = plugin_dir + os.sep + filename + ".zip"
        z = zipfile.ZipFile(zip_file_name, "w")
        for f in os.listdir(plugin_dir):
            if not f.endswith(".cs"):
                continue
            z.write(plugin_dir + os.sep + f)
        z.close()
        ret = send_file(zip_file_name, as_attachment=True, attachment_filename=filename + ".zip")
    else:
        ret = 'out type error'

    del_dir(plugin_dir)
    return ret


def del_dir(mdir):
    import time

    def del_async_wait():
        time.sleep(1)
        ct = 0
        while ct < 1000:
            try:
                shutil.rmtree(mdir)
                print('del  %s' % mdir)
                break
            except:
                ct += 1
                time.sleep(1)

    import threading
    t = threading.Thread(target=del_async_wait, name='LoopThread')
    t.start()


def read_text(path):
    f = open(path)
    content = f.read()
    f.close()
    return content


def run_plugin(plugindir):
    if not just_test:
        for f in os.listdir(plugindir):
            if not f.endswith('.proto'):
                continue
            if plugin_name == 'plugins3':
                name = os.path.splitext(f)[0]
                plugin = os.path.abspath(os.curdir) + '/%s/protoc.exe' % plugindir
                args = ' --csharp_out=./ ./%s.proto' % name
                p = subprocess.Popen(plugin + ' ' + args, cwd='%s/%s' % (os.path.abspath(os.curdir), plugindir))
                p.wait()
            else:
                name = os.path.splitext(f)[0]
                plugin = os.path.abspath(os.curdir) + '/%s/protogen.exe' % plugindir
                args = '-i:%s.proto -o:%s.cs' % (name, name)
                p = subprocess.Popen(plugin + ' ' + args, cwd='%s/%s' % (os.path.abspath(os.curdir), plugindir))
                p.wait()
    else:  # JUST _TEST
        for f in os.listdir(plugindir):
            if not f.endswith('.proto'):
                continue
            shutil.copy(plugindir + os.sep + f, plugindir + os.sep + f + ".cs")


def init():
    global just_test
    sys_str = platform.system()
    just_test = (sys_str != "Windows")


if __name__ == '__main__':
    init()
    http_server = WSGIServer(('0.0.0.0', 5001), app)
    http_server.serve_forever()
