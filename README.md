# Password_Manager
If you think about your passwords, you have 2 options(bad options); using the same password everywhere, use many different passwords that you wont remember.
So i created this project as a "minimal password manager".
The main idea is that you only have to remember one password that generates different passwords for each site , how?
With a private key and public keys.

Using this way anyone can have strong different passwords in each site using just one private key and the specific public key of the site.
For example suppose my private key is cat: 
so when i go to github on this program i calculate the password with cat as pk , and github as pk , it generates a password like this:
7RFLT4g4mLE4zAZLxjxBhw==0

Im working in a mobile simplified app, you can download the demos on demo-folder also you can acess anywhere with this link(https://dotnetfiddle.net/KLO8zk) 

Updated New Features:
Includes:
-Check the pk
-Get a cyphered number(some places only allow numbers) 

Credits to the library sharpromt, wich i used for some utilities.
Using SharePromt library: https://github.com/shibayan/Sharprompt
