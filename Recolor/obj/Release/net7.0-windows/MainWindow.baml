   M S B A M L   `   `   `  ���� B  >Recolor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:RecolorRecolor      Recolor.MainWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:Recolor       � Title$  
MainWindow��$	��800��$
��1200�� � 
ResizeMode$ NoResize=�.���  � ColumnDefinitions �� $
��121* ��� $��1079* �.+��� -   $��Open Pack Folder��$
��Left=�$��	26,10,0,0q�$	��Top=�$��25��$	��112��$��2���� -   $��1��$
��Left=�$��	43,10,0,0q�$	��Top=�$	��144��$��25��.@��� $	��1.8���� $
��1.20���� -   $��colorToShow��
���   �$	��112��$��59��$
��Left=�$��	26,55,0,0q�$	��Top=�$��2���� -   $��sl_red��$��2�� � Minimum$ 0Q� � Maximum$	 255Q�$
��Left=�$��
26,122,0,0q�$	��Top=�$	��112���� -   $��sl_green��$��2��$ 0Q�$	 255Q�$
��Left=�$��
26,147,0,0q�$	��Top=�$	��112���� -   $��sl_blue��$��2��$ 0Q�$	 255Q�$
��Left=�$��
26,173,0,0q�$	��Top=�$	��112���� $��R��$
��Left=�$��	4,120,0,0q�$	��Top=�$��20���� $��G��$
��Left=�$��	4,143,0,0q�$	��Top=��� $��B��$
��Left=�$��	5,169,0,0q�$	��Top=��� -   $��ch_browncolorFilter��$��Browncolor filter�� d� 	IsChecked$
 TrueY�$
��Left=�$��
23,205,0,0q�$	��Top=�$��22��$	��115��$��2���� -   $��lst_View_ItemsSelect��$��2��$��0,232,1014,0q��� -	   $��lv_defaultItems��$��156,84,718,20q�$��1���� $��1��$��Default Items:��$
��Left=�$��
156,55,0,0q�$	��Top=��� -
   $��lv_editedItems��$��586,84,278,19q�$��1���� $��1��$��Edited Items:��$
��Left=�$��
586,49,0,0q�$	��Top=��� -   $��1��$��Recolor��$
��Left=�$��
963,18,0,0q�$	��Top=�$��25��$��78���� -   $��1��$��Show recolor Example��$��Center=�$��0,13,0,0q�$	��Top=�$��26��$	��169��