# DetachETProject

#### 修改配置文件congfig.json,将里边的 RootPath定位到ET目录下的Server目录!!!!
 "RootPath": "C:\\Workspace\\VisualStudio\\ET\\Server"
#### 运行DetachProject.exe;
#### 已经可以正常使用Server了,打开server解决方案,重新编译即可;


#### ----------- 说明
本项目主要是通过解析server目录下的各个项目文件*.csproj,将里边引用到unity项目里边的文件复制到对应的server各个目录,然后删除多余节点.
