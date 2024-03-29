# Donquixote 🦩
Donquixote is a minimal SMS sender working with Line2 API.

* Donquixote sends **SMS** at various speeds to a **list of phone numbers** of your choosing.
* Donquixote allows you to send a **single defined message** to a **range** of numbers.
* Donquixote allows you to send a **ton of messages** to a **single or a range** of numbers.
* Donquixote supports **special characters, accents and emojis** within the message payload.

<p align="center">
  <img src="https://github.com/ecstasyspirit/Donquixote/blob/master/Donquixote/Images/donquixote.png" alt="Donquixote CLI" width="738">
</p>

## Usage 🤓

1. You must leave a text file named `numbers.txt` in the startup directory.
   Currently Donquixote only supports messaging Canada and U.S.A numbers.
   The numbers must follow a precise format and must be entrered one at a time on different lines.
   The international code by is added automaticlly and by default is `+1`. This cannot be changed.
   
    ```diff
    - This is bad formatting:
    - 5144-201-337
    - 514 420 1337
    - 15144201337
    
    + This is good formatting:
    + 5144201337
    + 4389476251
    + 4502800371
    ```
   
    ```diff
    + 00/00 | 00:00:00    Importing phone numbers from 'numbers.txt'... √
    ```
    
2. Set Donquixote connection to either `Direct` or `Proxy`.
   Selecting `Direct` is perfect for sending the SMS at a `Normal` or `Medium` speed.
   Selecting `Proxy` will allow you to safely choose the `Fast` or `Risky` speed without getting **IP banned**.

    ```diff
     00/00 | 00:00:00    Available connections: Direct, Proxy
     00/00 | 00:00:00    What connection do you want to use? Proxy
    + 00/00 | 00:00:00    Selected connection [Proxy].
    ```
    
    You must leave a text file named `proxies.txt` in the startup directory.
    The proxies must follow a precise format and must be entrered one at a time on different lines.
    The is proxy type is automatically detected and allows you to use mixed-type lists.
    
    ```diff
    - This is bad formatting:
    - 127.0.0.1 8888
    - 127.0.0.1, 8888
    - 127.0.0.1|8888
    
    + This is good formatting:
    + 127.0.0.1:8888
    + 127.0.0.1:8888
    + 127.0.0.1:8888
    ```
    
    ```diff
    + 00/00 | 00:00:00    Importing proxies from 'proxies.txt'... √
    ```

3. Set Donquixote mode to either `Spam` or `Bomb`.
   Navigate the options using the `Up` & `Down`/`Left` & `Right` keys of your keyboard and press `Enter` to confirm your selection.
   
    ```diff
     00/00 | 00:00:00    Available modes: Spam, Bomb
     00/00 | 00:00:00    What mode do you want to use? Spam
    + 00/00 | 00:00:00    Selected mode [Spam].
    ```
    
4. Set Donquixote speed to either `Risky`, `Fast`, `Medium` or `Normal`. 
   Do not go above select `Fast` or `Risky` without proxies as your **IP will be banned after 175 SMS**.
   `Medium` is the recommended speed to use in order to avoid either:

   * Getting your **account banned**
   * Getting your **IP banned**
   * Getting your **API key banned**
   
   Again, navigate the options using the `Up` & `Down`/`Left` & `Right` keys of your keyboard and press `Enter` to confirm your selection.
   
    ```diff
     00/00 | 00:00:00    Available speeds: Risky, Fast, Medium, Normal
     00/00 | 00:00:00    What speed do you want to use? Fast
    + 00/00 | 00:00:00    Selected speed [Fast] || pause between messages [500 ms].
    ```
    
5. Set the message payload to whatever message you want.
   Press `Enter` to confirm your input. Donquixote will display a preview of what your message payload will look like on the victims' devices.
   
    ```diff
     00/00 | 00:00:00    Set the message to use for the attack: Hello\n\nWorld! 👋
    + 00/00 | 00:00:00    This is how the message will appear on the victims' devices:
    +                 >>
    +                 Hello
    + 
    +                 World! 👋
    +                 <<
    ```
    
6. If you set Donquixote mode to `Bomb`, you will also need to set the messenger recursivity.
   This is the amount of time your message will be sent to each phone numbers of your list.
   Press `Enter` to confirm your input.
   
    ```diff
     00/00 | 00:00:00    Set the recursivity of the messenger to use for the attack: 50
    + 00/00 | 00:00:00    Recursivity parameter set to [50 times/phone number].
    ```
    
7. If you set Donquixote connection to `Proxy`, it is a crucial step to set the messenger recursivity.
   This is the amount of parallel requests that Donquixote will send out.
   If requests timeout, the default number of attempts that will be made for each phone number is 6.
   The amount of threads to use depends on the connection you chose earlier and the quality of your proxies.
   Below are recommended settings. They should just be taken as reference, increase or reduce the thread amount accordingly to your experience when messaging.
   These should work in most cases:

   * `Direct`: **1 to 2**.
   * `Proxy`: **150 to 250** if your proxies are free (scraped from the internet)
   * `Proxy`: **2 to 5** if your proxies are paid (private use proxies)

   Press `Enter` to confirm your input.
   
    ```diff
     00/00 | 00:00:00    Set the threads count to use for the attack: 150
    + 00/00 | 00:00:00    Thread parameter set to [150].
    ```
    
8. Set the phone number and password of your Line2 account.
   Donquixote will use this information to sign in your account to fetch your access token.
   This is a necessary step in order to use Donquixote.
   This will not work if your account doesn't have an active subscription to Line2 service.
   You will not see what you are typing but the console caret will move, that shows your information is correctly being recorded.
   
    ```diff
     00/00 | 00:00:00    Enter Line2 phone number:
     00/00 | 00:00:00    Enter Line2 password:
     00/00 | 00:00:00    Connecting ... √
    + 00/00 | 00:00:00    Fetched access token: xxxx:xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx.
    ```
    
9. Donquixote is ready to work. Simply press a key to debute.
    
    ```diff
    + 00/00 | 00:00:00    Press any key to start the attack ...
    ```
    
## Tips and tricks 🤩

You can use the **RegEx** entity `\n` in order to move to the **next line**.
Here are some exemples of its usage:
   
This
   
```
 Hello\nWorld! 👋
```
becomes that:
    
```diff
+ Hello
+ World! 👋
```
This
   
```
 Hello\n\nWorld! 👋
```
becomes that:
    
```diff
+ Hello
+ 
+ World! 👋
```
    
And so on.
