   M S B A M L   `   `   `  ����I  ESimpleCardGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:SimpleCardGameSimpleCardGame  5   	       SimpleCardGame.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:SimpleCardGame     5   	     � Title$  SimpleCardGame��5   	   $	��361��6    $	��453��6-    � 
ResizeMode$ CanMinimize=�69   ���	#FF4B4B4B � WindowStartupLocation$ CenterScreen=�6i   .��5	      � .+�5
   
   �� -   5         ��Name$ card_0��5
      $
��Left=�6    $	��160��6;   $��	35,60,0,0q�6H   $	��Top=�6[   $	��120��6s   $��Assets/a1.png��6    � Cursor$
 Hand��6�   �� 5   
   -   5        $ card_1��5      $
��Left=�6    $	��160��6;   $��
170,59,0,0q�6H   $	��Top=�6\   $	��120��6t   $��Assets/trump.png��6�   $
 Hand��6�   �� 5   
   -   5        $ card_2��5      $
��Left=�6    $	��160��6;   $��
306,60,0,0q�6H   $	��Top=�6\   $	��120��6t   $��Assets/a2.png��6�   $
 Hand��6�   �� 5   
   -   5         ��Name$ startBtn��5      $��Start��6#   $
��Left=�63   $��
35,248,0,0q�6N   $	��Top=�6b   $	��102��6z   $��36��6�   ���	#FF3CBD00
�������$��18(�6�   6�   �� 5   
   -   5         ��Name$ 	statusTxt��5      $
��Left=�6'   $��157,259,0,0q�6B   $
��Wrap=�6W   $	��---��6k   $	��Top=�6v   $��23��6�   $	��202��6�   
������� �� TextAlignment$ Center=�6�   $��16(�6�   �� 5   
   -   5         ��Name$ qLbl1��5      $��?��6   $
��Left=�6+   $��	76,-4,0,0q�6F   $	��Top=�6Y   ���	#FFFFF300$��48(�6�   $��63��6�   $��32��6�   �� 5   
   -   5        $ qLbl2��5      $��?��6   $
��Left=�6+   $��
213,-4,0,0q�6F   $	��Top=�6Z   ���	#FFFFF300$��48(�6�   $��63��6�   $��32��6�   �� 5   
   -   5        $ qLbl3��5      $��?��6   $
��Left=�6+   $��
347,-4,0,0q�6F   $	��Top=�6Z   ���	#FFFFF300$��48(�6�   $��63��6�   $��32��6�   �� 5   
   -	   5        $ scoreTxt��5      $
��Left=�6&   $��
10,290,0,0q�6A   $
��Wrap=�6U   $��Score:��6i   $	��Top=�6w   
�������$	��196��6�   $��Italic$�6�   $��16(�6�   $��21��6�   5      5      