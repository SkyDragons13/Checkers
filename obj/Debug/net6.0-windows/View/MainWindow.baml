   M S B A M L   `   `   `  ����C  ?Checkers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CheckersCheckers  5   	       Checkers.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Checkers     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��800��6)   .��5	      � .+�5   
    � -   5          �Name$ 	MainFrame��5        � NavigationUIVisibility$ Hidden=�6#   � 5   
   -   5         �Name$ Content��5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� 
�������$��7��6(   $��10q�69   $	��300��6E   ._�5      �� .+�5      �� -   5         ��Name$ 	NewButton��5      $��New Game��60   $��0��6f   $��Center=�6s   $	��Top=�6�   $��
10,10,10,5q�6�   $	��100��6�   ��6C   �� *NewGameCommand+6�   �� 5      -   5        $ 
OpenButton��5      $
��Open��62   $��1��6A   $��Center=�6r   $	��Top=�6�   $��	10,5,10,5q�6�   $	��100��6�   ��6N   �� *LoadGameCommand+6�   �� 5      -   5        $ 
SaveButton��5      $
��Save��61   $��2��6@   $��Center=�6q   $	��Top=�6�   $��	10,5,10,5q�6�   $	��100��6�   $y�	Collapsed=�6�   ��6M   �� *SaveGameCommand+6�   �� 5      -   5        $ 
StatButton��5      $��
Statistics��62   $��3��6G   $��Center=�6z   $	��Top=�6�   $��	10,5,10,5q�6�   $	��100��6�   ��6T   �� *StatisticsCommand+6�   �� 5       -   5        $ 
BackButton��5       $��Back to game��62   $��4��6I   $��Center=�6v   $	��Top=�6�   $��	10,5,10,5q�6�   $	��100��6�   $y�	Collapsed=�6�   ��6V   �� *BackCommand+6�   �� 5!      -   5        $ AboutButton��5!      $��About��63   $��5��6C   $��Center=�6q   $	��Top=�6�   $��	10,5,10,5q�6�   $	��100��6�   $y�	Collapsed=�6�   ��6P   �� *AboutCommand+6�   �� 5"      -	   5         ��Name$ Checkbox��5"      $��Allow multiple jumps��61   $��6��6P   $��Center=�6]   $	��Top=�6z   $��
10,5,10,10q�6�    d� 	IsChecked 6�   �� *IsMultipleJumpsEnabled+ �� Mode$ TwoWay=�6�   5#      5$      5%      5&      5'      