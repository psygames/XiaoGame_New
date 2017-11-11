#!/bin/sh
cd `dirname $0`
echo 移除 MainServer/Protocol
rm -rf ../../Server/MainServer/Protocol
echo 移除 BattleServer/Protocol
rm -rf ../../Server/BattleServer/Protocol
./protogen
echo 按任意键结束
read -n 1
