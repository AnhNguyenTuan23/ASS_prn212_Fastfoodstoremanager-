   M S B A M L   `   `   `  ����L  HPRN212_ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PRN212_ProjectWPFPRN212_ProjectWPF  5      *    $PRN212_ProjectWPF.CustomerManagement   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PRN212_ProjectWPF     5      .��5	      � 
�������.+�5
   
   � 
�������  � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��Center=�6   .+�5      �� $��30(�6   $
��Bold"�6*   Customer Management6Q   5      � 5      $��1��6   $��10q�6     � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $��* �6'   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5      �� -   5         ��Name$ 	txtSearch��5      $	��200��62   
���   �6R   �� 5      -   5         ��Name$ 	btnSearch��5      $��Search��60   $
��Bold"�6A   $��30��6S   $��0,0,5,0q�6_   5      �� 5      $ 
Horizontal=�6   $��1��66   $��Right=�6F   .+�5      �� -   5        $ btnAdd��5      $	��Add��6-   $��60��6;   $
��Bold"�6F   $��0,0,20,0q�6m   �� 5      -   5        $ 	btnUpdate��5      $��Update��60   $��60��6A   $
��Bold"�6L   $��0,0,20,0q�6_   6q   5       5!      &   System.Windows.Controls.DataGrid  5"      -   5          Name$ dgCustomerManagement��5"   $   $��2��6      AutoGenerateColumns$ False��6B      CanUserAddRows$ False��6^      SelectionMode$ Single=�6u   	   Columns	 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 6)      Header$
 Name��66   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *CustomerName+  5%      $
 * 6)   $ Email��63    6B   �� *Email+  5&      $
 * 6)   $ Phone��63    6B   �� *Phone+  5'      $

 Auto 6)   $ Gender��66    6F   �� *GenderDetail+  5(      $

 Auto 6)   $ Status��66    6F   �� *StatusDetail+5*      5+      5,      5-      