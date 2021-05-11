# 一、Linux简介
## 1.Linux基础简介
---
- Linux  
    1. 1991年，芬兰人林纳斯.托瓦尔兹在赫尔辛基大学自己动手写了操作系统，就是Linux内核（Linux Kernel）。
    2. 吉祥物为企鹅。
    3. Linux加入GNU并尊徐公共版权许可证。

- GNU （“GNU's Not Unix”）  
    1. 理查德.斯托曼博士1983年启建立一个完全想让与Unix的自由软件环境。
        + 自由软件。
        + 赋予用户复制、研究、修改和散布该软件的权利，并提供源码供用户自由使用。
- GPL（General Public License 通用公共许可证）
    1. 为了避免GNU开发的软件被其他人利用称为专利软件。
    2. Copyleft（专利软件是Copyright）。
    3. 目的要让GNU永远是免费和公开的。
- Linux简介
    1. Linux是一种自由和开放源码的类Unix操作系统。
    2. Linux的特点
        + 开放性，多用户，多任务，丰富的网路功能。
        + 可靠的系统安全，良好的移植性，命令界面，出色的速度性能。
    3. Linux的组成
        + 内核
        + shell
        + 文件系统
        + 引用程序
    4. Linux版本
        + Linux内核（linux kernel）
        + 发行版本
            * 厂商将Linux系统内核与应用软件和文档包装起来，并提供一些安装界面和系统设定管理工具的一个软件包的集合
            * 常见的版本有 Redhat，Debian，Ubuntu，Novell/SuSe Linux等。例如：Ubuntu的版本有发布的年份和月份组成最新的版本：Ubuntu 12.10
## 2.Linux系统安装
---
- 下载ios系统镜像文件刻录光盘，傻瓜式安装（直接安装），或者可以用虚拟安装
    + [网易镜像](http://mirrors.163.com/)
    + [阿里镜像](http://mirrors.aliyun.com/)
    + 以阿里的举个例子
        * 拉到下边，选择**ubuntu-releases**点击进去  
    ![目录选择](https://images2017.cnblogs.com/blog/815062/201712/815062-20171230144849663-1126861648.png)
        * 进去之后这里有很多版本，我选择的是16.04，点击进入![版本选择](https://images2017.cnblogs.com/blog/815062/201712/815062-20171230144935960-1562658670.png)
- Vmware虚拟机安装
    + 虚拟机（[Virtual Machine](https://www.vmware.com/cn.html)）是一个软件，可以模拟出具有完成硬件系统功能的，运行在一个完全隔离环境的中计算机。
    + 直接一路下一步，安装完成。
    + 注意：安装完虚拟机后，进入网上邻居->属性->，禁用
     **VMware Network Adapter VMnet1** 和 **VMware Network Adapter VMnet8**
- Linux的命令行与图形界面切换
    + 命令行界面
        * 在图形化界面中打开命令行窗口：应用程序->附件->终端
        * 切换到纯命令行使用`Ctrl+Alt+F1~F6`
    + 图形用户界面（`Graphical User Interface`，简称GUI）
        * 使用`Ctrl + Alt + F7 `切换到图形用户界面
- 普通用户与超级用户
    + 显示`$`标识的标识是普通用户，例如：进入控制台出现：`itcast@ubuntu:~$`，标识在普通用户`itcast`的根目录（`/home/itcast`），`~`代表用户根目录
- Linux目录结构：
    + Linux只有一个盘，`/`代表是根目录，所有的文件都在根目录中
    + 其他目录
        * `bin`：存放二进制可执行文件（ls，cat，mkdir这些命令和其他一些可执行的命令等）
        * `boot`：存放用于系统引导时使用的各种文件
        * `dev`：用于存放设备文件（dev是device的简写，就是设备的意思，Linux把每个硬件也看作是一个文件
        * `etc`：存放系统配置文件，例如，当前系统用户的配置，jdk的安装配置，环境变量什么的
        * `home`：存放所有用户文件的根目录
        * `mnt`：系统管理员安装临时文件系统的安装点
        * `opt`：额外安装的可选应用程序包放置的位置，相当于我们windows中自定义安装软件的位置
        * `root`：超级用户目录
        * `sbin`：存放二进制可执行文件，只有root才能访问
        * `usr` 用于存放系统应用程序，类似windows中的Program Files，例如：软件中心下载的软件默认安装在usr/bin中，我们也可以将jdk安装在此目录
# 二、Linux常用命令
## 1.开关机命令
- 注销：`logout` 或 `exit`
- 关机：`halt` 或 `shutdown -h now`（要是root用户或者有授权才可以）
- 重启：`reboot` 或 `shutdown -r now` （要是root用户或者有授权才可以）
## 2.Linux的基本命令
- `ls` 显示文件和目录列表
    + `-l` 列出文件的详细信息，如：drwxr-xr-x  2 songlin songlin 4096 Jun  5 21:53 Desktop
        * 第一个字母标识文件类型，d标识目录 (-:普通文件，p：管理文件，l：连接文件，b：块设备文件，c：字符设备文件，s：套接字文件)
        * 后边三个字母为一组，前三个标识文件所有者权限  
          中间三个标识组用户权限（一个组中除所有者拥有的权限）  
          其他用户权限（除过当前所有者的组，其他组可以进行访问的权限）  
          文件权限：r 读权限，w 写权限，x 可执行权限，- 无权限
        * 数字2：对于普通文件表示连接数，对于目录表示子目录数（文件夹默认有两个隐藏的子目录.和..，所以对于文件夹来说，这个数字最少是2)
        * 第一个songlin：表示用户名
        * 第二个songlin：表示组名
        * 4096：文件大小（单位：字节）
        * Jun  5 21:53：最后修改时间
        * Desktop：文件或文件夹名称
    + `-a` 列出当前目录所有文件，包含隐藏文件(ls内定将文件名或目录名称开头为"."的视为隐藏档，不会列出) 
- `mkdir` 创建目录
    + `rmdir` 删除目录，需要非空
    + `-p` 父目录不存在的情况下生成父目录
- `cd` 切换目录
    + `cd ..` 切换到上一级目录
    + `cd ../..` 切换到上一级的上一级目录，以此类推
- `touch` 生成一个空文件
    + `touch a.txt` 生成一个a.txt空文件
- `echo` 输出内容到xxx
    + `echo abcd>a.txt` 输出内容到a.txt，相当于往a.txt中写入内容abcd，该方式为覆盖写入
    + `echo hijk>>a.txt` 两个`>`表示追加方式写入
- `cat` `tac` 显示文本文件内容
    + cat命令可以显示文件的内容，它反过来写就是tac，而tac恰巧也是一个Linux命令，它的功能就是把文件内容反过来显示，文件内容的最后一行先显示，第一行最后显示。Linux世界的人们好玩吧！
- `cp` 复制文件或目录
    + `cp a.txt /home/itcast/adc/ddd` 把a.txt复制到该目录下
- `rm` 删除文件
    + `rm -rf abc` 强制递归删除adc目录下的所有内容
- `mv` 移动文件或目录
    + `mv aaa bbb` 将aaa改名为bbb
    + `mv bbb /home/itcast/asb/ccc` 将bbb移动到该目录下
- `find` 在文件系统中查找指定的文件
    + `find -name 文件名` 
- `wc` 统计文本文档的行数，字数，字符数
    + `wc a.txt` 统计a.txt的行数，字数，字符数
- `grep` 在指定的文本文件中查找指定的字符串
    + `grep aa a.txt` 在a.txt中查找aa
- `pwd` 显示当前工作目录
- `ln` 建立连接文件
    + `ln -s /home/itcast/familyA/house/roomB   /home/roomB `
        * 当访问一个目录较深的文件，可以建立链接文件
        * 在home下就可以直接访问roomB的文件
        * 例如安装jdk路径需要配置环境变量，如果路径较长书写麻烦可以配置链接文件
        * `ln`命令会保持每一处链接问价你的同步性，也就是说，不管你改动了哪一处，其他的文件都会发生相同的变化
        * `ln`链接又分为软链接和硬链接两种，软链接就是ln –s ** **，它只会在你选定的位置上生成一个文件的镜像，不会占用磁盘空间，硬链接ln ** **，没有参数-s， 它会在你选定的位置上生成一个和源文件大小相同的文件，无论是软链接还是硬链接，文件都保持同步变化。
        * 如果你用ls察看一个目录时，发现有的文件后面有一个@的符号，那就是一个用ln命令生成的文件，用ls –l命令去察看，就可以看到显示的link的路径了。
- `more、less` 分页显示文本文件内容
    + 查看配置文件时，很长需要分页处理
    + more（一页一页翻）
        * 空格键向下翻页
        * Enter键向下滚动一行
        * :f 显示出文件名及当前的行数
        * q 离开more
        * b 往回翻
    + less（一页一页翻）
        * 空格 向下翻一页
        * PageDown 向下翻一页
        * PageUp 向上翻一页
        * q 离开
- `head、tail` 分别显示文件开头和结尾内容
- `man` 命令帮助信息查询
    + `man ls` 查看ls命令的帮助信息
- `|` 管道命令（上一个命令的输出作为下一个命令的输入）
    + `cat /etc/passwd | wc -l` 使用cat命令显示passwd文件中的内容，但是并没有显示在屏幕上，而是通过管道“|” 接受，wc命令从管道中取出内容进行统计，然后显示结果
				这个输出时该文件有多少行（多少个用户）
- `>、>>` 重定向
    + `>` cat /etc/passwd>/home/itcast/a.txt  
    	echo "hello java">a.txt  （覆盖上一个a.txt）
    + `>>` 追加，不会覆盖，cat /etc/passwd>>/home/itcast/a.txt   
        echo "---------">>a.txt 
## 3.Linux系统命令
- `stat` 显示指定文件的相关信息
    + `stat a.txt`
        * 说明：stat - display file or file system status
        * access 进入，Modify 修改，Change 改变
        * access time是文档最后一次被读取的时间。因此阅读一个文档会更新它的access时间，但它的modify时间和change时间并没有变化。cat、more 、less、grep、tail、head这些命令都会修改文件的access时间。
        * change time是文档的索引节点(inode)发生了改变(比如位置、用户属性、组属性等)；modify time是文本本身的内容发生了变化。[文档的modify时间也叫时间戳(timestamp)
- `who` 显示在线登录用户
    + 想要知道当前有多少用户登录系统。
- `whoami` 显示用户自己的身份 
- `hostname` 显示主机名称
    + hostname
    + hostname -i 显示主机IP
- `uname` 显示系统信息
    + uname -a 显示全部信息  
    如：Linux ubuntu 2.6.35-22-generic #33-Ubuntu SMP Sun Sep 19 20:34:50 UTC 2010 i686 GNU/Linux
- `top` 显示当前系统中耗费资源最多的进程 动态显示过程，实时监控
    + 类似于windows的任务管理器
    + 主要看 cpu mem command
    + ctrl+c 退出，或者q
- `ps` 显示瞬间进程状态
    + ps -aux  显示所有瞬间进程状态
- `du` 显示指定的文件（目录）已使用的磁盘空间的总量
    + du
    + du aa.txt（以K为单位）
    + du -h aa.txt
- `df` 显示文件系统磁盘空间的使用情况 
    + df -h
- `free` 显示当前内存和交换空间的使用情况
- `ifconfig` 显示网络接口信息
    + windows 是ipconfig
- `ping` 测试网络的连通性
- `clear` 清屏
- `kill` 杀死一个进程
- 关机/重启命令
    + `shutdown` 命令可以安全的关闭Linux系统，shutdown命令必须有超级用户才能执行。shutdown命令执行后会以广播的形式通知正在系统中工作的所有用户
        * shutdown  -h now  （关机不重启）
        * shutdown  -r now  （关机重启）
        * shutdown  now （关机）
        * shutdown  15:22
    + `halt` 关机后关闭电源
    + `reboot` 重新启动
## 4.备份压缩打包命令
- `tar` 打包拆包
    + 参数
        * `-c`：建立一个归档文件的参数指令（打包）
        * `-x`：解开一个归档文件的参数指令（拆包）
        * `-z`：是否需要进行gzip压缩
        * `-j`：是否需要进行bzip2压缩
        * `-v`：压缩过程中显示文件
        * `-f`：使用的文件名称
        * `-tf`：查看归档文件里边的文件
    + 打包：
        * tar -cvf familyA.tar familyA （打包familyA目录成familyA.tar）
        * tar -cvf familyA.tar *.txt （打包当前目录下的所有.txt文件成familyA.tar）
        * tar -cvf familyA.tar * （打包当前目录下的所有文件成familyA.tar，目录会自动忽略）
    + 拆包：tar -xvf /home/itcast/familyA.tar 
- `gzip` 压缩（解压）文件，压缩文件后缀为gz 
    + 压缩
        * 把/home/itcast目录下的familyA目录下所有文件压缩成.gz文件
        * gzip只能压缩文件，目录（文件夹不能处理），需要使用tar对文件夹打包
        * gzip familyA.tar 进行压缩
    + 查看压缩文件
        * gzip -l familyA.tar.gz 查看压缩包详细信息
        * compressed 压缩后大小
        * uncompressed 原始大小
        * ratio  压缩比
        * uncompressed_name  原始文件名
    + 解压
        * gzip -d familyA.tar.gz   显示文件名和压缩比
    + 压缩比
        * 高压缩（速度稍慢）  
        gzip -9 familyA.tar 高压缩比  
        gzip -l familyA.tar.gz 
        * 低压缩比（速度快）  
        gzip -d familyA.tar.gz （解压）  
        gzip -1 familyA.tar 低压缩比  
        gzip -l familyA.tar.gz
        * 默认压缩等级：6
- `bzip2` 压缩（解压）文件或目录，压缩文件后缀为bz2 
    + 压缩 
        * 把/home/itcast目录下的familyA目录下所有文件压缩成.bz2文件
        * bzip2 -z familyA.tar 压缩需加上参数-z
    + 解压缩
        * bzip2 -d familyA.tar.bz2
- `tar` 压缩和解压
    + 仅打包，不压缩
        * tar -cvf familyA.tar familyA
    + 打包后，以gzip压缩
        * tar -zcvf familyA.tar.gz familyA拆包
        * sudo tar -zxvf familyA.tar.gz
    + 打包后，以bzip2压缩
        * tar -jcvf familyA.tar.bz2 familyA拆包
        * sudo tar -jxvf familyA.tar.bz2 
# 三、软件管理
## 1.软件安装
- 直接安装.deb包 dpkg软件包
    + 安装以.deb结尾的软件包，需要使用root的权限
        * sudo dpkg -i 软件包名
    + 卸载 
        * sudo dpkg -r 软件包名
    + 看Ubuntu系统已安装所有软件包列表
        * sudo dpkg -l
    + 很多情况软件安装:/usr/local之下
- 使用apt-get管理软件（推荐使用）
    + 前提连接互联网，自动搜素，自动下载自动安装
    + 安装
        * sudo apt-get install eclipse
        * sudo apt-get install openjdk-6-jdk
    + 卸载
        * sudo apt-get remove packagename
    + 使用
        * 默认下载路径 /var/cache/apt
        * 同时也可以可以更新和搜索软件
        * 执行“apt-get update”更源列表
        * 执行“apt-cache search 名称”搜索软件
    + ubuntu系统如何查看软件安装的位置
        * dpkg -L  软件名
        * 软件中心下载的软件默认保存路径：/var/cache/apt/archives.
## 2.编辑
- vim编辑器
    + 运行模式
        * 编辑模式：等待编辑命令输入
        * 插入模式：编辑模式下，输入 i 进入插入模式，插入文本信息
        * 命令模式：在编辑模式下，输入 “：” 进行命令模式	
    + 安装
        * 安装进入入file文件 cd /home/itcast/Desktop/file
        * sudo dpkg -i vim-runtime_7.2.330-1ubuntu4_all.deb
        * sudo dpkg -i vim_7.2.330-1ubuntu4_i386.deb 
        * 检查安装成功 敲入vim即可 退出
    + 使用vim编辑
        * vim aa.txt
        * 数据命令i 进入插入模式
        * 输入内容 sfdaf
        * ctrl+C 退出插入模式或者敲ESC切换至命令模式
        * :wq 回车 保存
        * :q! 回车 强制退出
        * cat aa.txt  使用cat查看内容
    + 显示行号
        * vim aa.txt	
        * :set number 回车
        * :q 回车 正常退出
    + 取消行号
        * :set nonumber


   


    


    

    