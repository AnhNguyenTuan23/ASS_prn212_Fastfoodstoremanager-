   M S B A M L   `   `   `  ����L  HPRN212_ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PRN212_ProjectWPFPRN212_ProjectWPF  5   	   &     PRN212_ProjectWPF.CustomerWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PRN212_ProjectWPF     5   	     � Title$  CustomerWindow��5   	   $	��450��6    $	��800��6-   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��4* �6   .+�5   
   � 
������� � RowDefinitions 5      �� 5      $��* �6    6-   �� 5      $��* �6    6-   �� 5      $��* �6    6-   .+�5      �� -   5         ��Name$
 logo��5      $��Images/logo.webp��6"   �� 5      $��1��6   .+�5      �� -   5         ��Name$ btnMenu��5      $�� Order��6*   
��� ���6O   �� 5      -   5        $ 
btnProfile��5      $��Profile��6-   $��0,10,0,0q�6?   
��� ���6e   �� 5      -   5        $ 
btnHistory��5      $��Order History��6-   $��0,10,0,0q�6E   
��� ���6k   5      5      �� 5   
   $��Bottom=�6   $��2��60   .+�5      �� -   5        $��Logout��5      $��
30,0,30,20q�6&   $��5q�6:   6F   5      �� 5    
   -   5        $��MainContentControl��5    )   $��1��6   6)   5!      5"      