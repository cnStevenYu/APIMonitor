# APIMonitor

Using EasyHook to monitor apis which the program called. </br>
EasyHook has four levels. From the bottom up, there are asm level, unmanaged code level, managed code level and your application level.

#How does EasyHook work?
![easyhook](https://github.com/cnStevenYu/APIMonitor/blob/master/Resource/easyhook.png?raw=true)
####Note:</br>
1.指令边界问题；</br>
  API指令开头被覆盖的指令长度至少是5个字节，需要知道开头被覆盖的指令是占用多少个字节，从而计算下一条指令的地址。</br>
2.被覆盖指令的重定位问题；</br>
  hook->oldProc存放被覆盖的指令，其中的call/jmp指令需要重新定位。</br>

tag: Hooking、dll injection
