   M S B A M L   `   `   `  ����L  HPRN212_ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PRN212_ProjectWPFPRN212_ProjectWPF  5   	   #    PRN212_ProjectWPF.AdminWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PRN212_ProjectWPF     5   	     � Title$  AdminWindow��5   	   $	��450��6   $	��800��6*   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��4* �6   .+�5   
   � 
������� � RowDefinitions 5      �� 5      $��* �6    6-   �� 5      $��* �6    6-   �� 5      $��* �6    6-   .+�5      �� -   5         ��Name$
 logo��5      $��Images/logo.webp��6"   �� 5      $��1��6   .+�5      �� -   5         ��Name$ btnChangePassword��5      $�� Change Password��64   
��� ���6c   �� 5      -   5        $ btnMenu��5      $��0,10,0,0q�6*   $�� Menu��6=   
��� ���6a   �� 5      -   5        $ btnTable��5      $��Table Management��6+   $��0,10,0,0q�6F   
��� ���6l   �� 5      -   5        $ btnEmployee��5      $��Employee Management��6.   $��0,10,0,0q�6L   
��� ���6r   �� 5      -   5        $ btnCustomer��5      $��Customer Management��6.   $��0,10,0,0q�6L   
��� ���6r   �� 5      -   5        $ 	btnBranch��5      $��Branch Management��6,   $��0,10,0,0q�6H   
��� ���6n   �� 5      -   5        $ 	btnReport��5      $��Static Report��6,   $��0,10,0,0q�6D   
��� ���6j   5      �� 5       $��Bottom=�6   $��2��64   .+�5!      �� -	   5        $��Logout��5!      $��
30,0,30,20q�6*   $��5q�6>   6J   5"      5#      �� 5$   
   -
   5        $��MainContentControl��5$   )   $��1��6   6)   5%      5&      