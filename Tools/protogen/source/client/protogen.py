import requests
import configparser
import os
import zipfile
import shutil
from contextlib import closing
from tqdm import tqdm


def main():
    cfg = configparser.ConfigParser()
    cfg.read("config.ini")
    url = cfg.get("Setting", "url")
    proto_dir = cfg.get("Setting", "proto_dir")
    save_dir = []
    for i in range(0, 10):
        if not cfg.has_option("Setting", "save_dir_%s" % i):
            continue
        _dir = cfg.get("Setting", "save_dir_%s" % i)
        save_dir.append(_dir)
        if not os.path.exists(_dir):
            os.mkdir(_dir)

    url = '%s?type=zip&out_type=zip&filename=zip' % url
    zip_path = proto_dir + os.sep + "zip.zip"
    z = zipfile.ZipFile(zip_path, "w")
    for f in os.listdir(proto_dir):
        if not f.endswith(".proto"):
            continue
        z.write(proto_dir + os.sep + f)
    z.close()

    files = {'file': open(zip_path, 'rb')}

    if not os.path.exists("download"):
        os.mkdir("download")
    download_path = "download/zip.zip"
    download(url, files, download_path)

    f = zipfile.ZipFile(download_path, 'r')
    for file in f.namelist():
        f.extract(file, "download/")
        if file.endswith('.cs'):
            for cp_dir in save_dir:
                shutil.copy("download" + os.sep + file, cp_dir + os.sep + os.path.basename(file))

    f.close()
    files['file'].close()
    shutil.rmtree("download")
    os.remove(zip_path)

    print('文件生成完成！')


def download(url, up_files, save_path):
    print('文件上传中...')
    with closing(requests.post(url, files=up_files, stream=True)) as response:
        print('远程生成完成，开始下载...')
        chunk_size = 1024  # 单次请求最大值
        content_size = int(response.headers['content-length'])  # 内容体总大小
        pbar = tqdm(total=content_size / 1024, desc='进度', unit='Kb', unit_scale=True)
        with open(save_path, "wb") as file:
            for data in response.iter_content(chunk_size=chunk_size):
                file.write(data)
                pbar.update(len(data) / 1024.0)
        pbar.close()


if __name__ == '__main__':
    main()
    os.system('pause')
