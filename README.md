<h1>UNO <img src="https://i.imgur.com/e4ooRT0.png" width="25" height="40"/></h1>
<p>This is a recreated version of the card game UNO using WPF and the .NET framework. This was created for a school project and I created every single aspect of it, including images and audio (except for background music and sparkle effect).</p>
<hr/>
<h3>About</h3>
<p>
  UNO is a simple card game where each player starts with 7 cards thats consist of cards that can be red, blue, yellow, or green, and be a basic number card or a special card that allows you to skip a turn, pick up 2, pickup 4, colour swap, or reverse the order of play.
  To play a card, you can only play 1 card each turn, and the card must be either the same colour and/or card type of the current card on top of the discard pile. Another card that can be played are the black cards, which can be played on top of any card.
  <img src="https://i.imgur.com/1iYTnpq.png"/>
</p>
<h3>Known Bug</h3>
<p>There is 1 single bug, which is a structural bug and can only b fixed by restructuring the code of the app. When creating this project, I used this to be my async function learning project. The bug is related to a timing issue with a synchronous and asynchronous function accessing the same variable at the same time.<br/>
I have gone through multiple testing and came to the conclusion its solely based on a timing issue. The bug causes the app to crash, but the bug could occur at any point in time, ranging from the first time playing a card to a bot playing a card 6 games deep into the app.</p>
