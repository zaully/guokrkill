Judge role: 00000000 -> 0
Villager: 00000001 -> 1
Hunter: 00000010 -> 2
Priest: 00000100 -> 4
Hunter:	00001000 -> 8

-------------------------------------------------------------------------------------------
大体的介绍：

每天收集完技能以后，开始按顺序计算：

牧师的技能
狼人技能：若目标死亡，技能无效果
猎人技能：若目标死亡，技能无效果
拜访技能：若目标有行动，返回不在家

-------------------------------------------------------------------------------------------
ToDo List:
修正狼吃狼，猎枪猎，


--------------------------------------------------------------------------------------------
数据库目标备忘：
012345678
8 村民					1
7 猎人					2
6 牧师					4
5 狼人					8
4 城主					16
3 暂缺					32
2 =1指可以施放给别人	64
1 =1指可以施放给自己	128
0 =1指目标不提前确定	256


1	吞噬		8	71	.90	1
2	感染		8	71	.85	1
3	银弹		2	77	.95	1
4	圣水		2	130	.90	1
5	火刑		16	79	1.00	1
6	守护		2	271	.80	0
7	拜访		15	79	1.00	1
8	指控		15	271	1.00	1
9	净化		4	75	.90	1
10	祈祷		4	132	1.00	1
11	神佑		4	75	1.00	1
12	奉献		4	132	1.00	1


-------------------------------------------------------------------------------------------
Outdated or obsolete:

Guard	+	Succeeded?	Put Guard section to yes	if triggered, put guard section to no
Visit	+			
Consecration				
Prayer	+		Modify Buff section	
Purge	+		Clean infestation section	
Blessing	+		Modify Buff section	
Devourment	+			if the victim is hunter clear the relevant guard section
Compass				
Holy Water	+			
Infestation	+			if the victim is hunter clear the relevant guard section
Silver Bullet	+			if the victim is hunter clear the relevant guard section
Inheritance
--------------------------------------------------------------------------------------------