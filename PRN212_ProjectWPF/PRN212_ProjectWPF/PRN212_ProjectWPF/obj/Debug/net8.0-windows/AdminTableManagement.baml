   M S B A M L   `   `   `  ����L  HPRN212_ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PRN212_ProjectWPFPRN212_ProjectWPF  5      ,    &PRN212_ProjectWPF.AdminTableManagement   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PRN212_ProjectWPF     5      .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      $��4* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��2��6   $��Center=�6)   .+�5      �� $��TABLE MANAGEMENT��6   $
��Bold"�60   $��30(�6B   5      �� 5   
   $��1��6   $��2��6"   .+�5      � .+�5      �� -   5         ��Name$ 	cbxBranch��5      6.   �� 5      -   5         ��Name$ btnAdd��5      $	��Add��6)   $��5q�67   $��	10,0,10,0q�6C   6V   �� 5      -   5        $ 	btnDelete��5      $��Delete��6,   6=   5      5      �� 5   
   $��2��6   $
��Auto=�6$   .��5      � -   5         �Name$ wpTables��5       � 	Resources 5       �� 
TargetType �� Property �� Value��%  '��       �� 5       " ��6   .�5!      ��  � ��$	 100��62   �� 5"       � ��$	 100��63   �� 5#       � ��
 �����67   �� 5$       � ��$ 20(�65   �� 5%       � ��$
 Bold"�67   �� 5&       � ��$ 5q�63   5'      5(      5)      5*      �� 5+   
   $��2��6   $��1��6"   .+�5,      �� $��50��6   ���	#FFF3A222.+�5-      �� -   5        	 ��Name$	 txbTable��5-      $��Table��6,   $��30(�69   $
��Bold"�6G   
���   �$��Center=�6l   $��Center=�6�   6�   5.      �� 5/      $��1��6   $
��Auto=�6(   $
�� 300��6K   .��50      &   System.Windows.Controls.DataGrid  -   5        $��
dgvDisplay��50   N   
   AutoGenerateColumns$
 False��6      CanUserAddRows$ False��67      Columns 51      0  *System.Windows.Controls.DataGridTextColumn  52      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6-      Header$
 Name��67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Dish.DishName+ �� Mode$ OneWay=�  53      $
 Auto 6-   $ Number��6:    6J   �� *
Quantity+$ OneWay=�  54      $
 Auto 6-   $ Price��6:    6I   �� *
Dish.Price+$ OneWay=�56      57      � 58      $��10q�6   .+�59      �� $��Total:��6   �� 5:      -   5         ��Name$ txbTotal��5:   .   $��50,0,0,0q�6   ��6@   �� *Order.TotalPrice+5;      5<      5=      5>      