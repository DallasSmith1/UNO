   M S B A M L   `   `   `  ����>  :UNO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UNOUNO  5   	       UNO.LobbySearch   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:UNO     5   	   -   5          � Title$  LobbySearch��5   	   $	��450��6   $	��571��6*   ���	#FF476F36 � WindowStartupLocation$ CenterScreen=�6M   $	��572��6�   $	��572��6�   $	��450��6�   $	��450��6�   .��5	      � .+�5
   
   �� -   5         ��Name$ cvsExplorer��5
      .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgrLobbies��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6V    �� Left$ 10��6{   
 �� Top$ 40��6�   $	��276��6�   $��Center=�6�   $	��384��6�   $	��Top=�6�   �� 5      -   5         ��Name$ btnHost��5      $
��Host��6&   $
��Left=�65   $��
340,73,0,0q�6P   $	��Top=�6d   $��48��6|   $	��168��6�   
���  ��6�   �� 5      -   5        $ btnJoin��5      $
��Join��6&   $
��Left=�65   $��306,284,0,0q�6P   $	��Top=�6e   $��55��6}   $	��235��6�   
���  ��6�   �� 5      $��Join Selected Lobby��6   $
��Left=�62   $��356,245,0,0q�6M   $	��Top=�6b   $	��132��6z   �� 5      -   5        $ 
btnRefresh��5      $��Refresh��6)   $	��180��6;   
���  ��$
��Left=�6X   $	 106��6s   $ 10��6�   $��Center=�6�   �� 5      -   5        $ btnBack��5      $
��Back��6&   $ 10��65   $ 10��6F   $
��Left=�6V   $��Center=�6q   $��91��6�   
���  ��6�   �� 5      -   5         ��Name$ lblIP��5      $��	Your IP: ��6#   $	 340��67   $ 42��6I   $��Center=�6Y   $	��Top=�6v   $	��168��6�   5      �� 5   
   -	   5        $ cvslobby��5      $y�Hidden=�6#   .+�5        -
   5        $ 
dgrPlayers��5      $	��124��6+   $	��196��68   $	 188��6o   $ 76��6�   �� 5      $��Players��6   $	 262��6&   $ 45��68   $
��Left=�6H   $��Center=�6c   �� 5      -   5        $ btnStartMatch��5      $��Start Match��6,   $	 326��6B   $	 260��6T   $��54��6e   $	��113��6q   $
��Left=�6}   $��Center=�6�   �� 5      -   5        $ btnLeaveLobby��5      $��Leave��6,   $	 133��6<   $	 259��6N   $��55��6_   $	��114��6k   5      5      5      