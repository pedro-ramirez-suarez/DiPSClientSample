# DiPS Client Samples

##Sample applications for the DiPS service
<a href="http://pedro-ramirez-suarez.github.io/DiPS/">Download DiPS service >></a>

To run this samples you need to have a DiPS service running in your network, some of the samples asume that you have a DiPS service runnin on:
192.168.56.101 on port 8888 others asume that you are running the service on localhost, in the same 8888 port, modify those depending on your environment.

There are 3 code samples on C# and two on ruby, you will need the dipsclient ruby gem to run the samples, to install the gem just type:
<pre>gem install dipsclient</pre>

C# Samples:
- A simple web application that launch searches and receives search results, to run this, you need to run the WordCount sample, works with  the ruby or the C# version. 

- A word count, this console app, receives a word, and search how many times the word is found in all .txt files in some folder.

- A Chat application, a simple chat application that uses gravatar to display avantars.


Ruby Samples:

- A word count, this console app, receives a word, and search how many times the word is found in all .txt files in some folder.

- Echo sample, a simple app that sends a message and receives the same message and prints it to the screen.
