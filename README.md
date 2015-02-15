# UER AvChat Client #

Unofficial chat client for [UER](http://www.uer.ca) written in C#/.NET. This project is on hiatus for a while. The code and resulting software contained herein are
MIT licensed. Anyone is free to fork it and continue their own project/implementation.

## Structure ##

**Crypton.AvChat.Client** - primary library (.dll) that implements tcp socket and AvChat protocol. Contains ChatClient class that handles the connection
as well as dispatching events (e.g. Message Received).

**Crypton.AvChat.Gui** - Windows Application that is the desktop chat client (references the Client library above).

**Crypton.AvChat.Win** - Incomplete effort to convert aforementioned Windows Application client into a [WPF](https://en.wikipedia.org/wiki/Windows_Presentation_Foundation) application.

## Compiling ###

Build `Crypton.AvChat.sln` (in Visual Studio or MSBuild), refer to `bin\[Debug|Release]` directories for executables.

## ClickOnce / Update Note ##

The Windows Application chat client is setup with [ClickOnce](https://en.wikipedia.org/wiki/ClickOnce) deployment (publish) to receive application
updates from URL `http://app.crypton-technologies.net/uer/avchat/`

While I still support hosting a website and the last released official version of the chat client, I don't forsee me supporting
this project. It is strongly recommended that you remove auto-update mechanism (e.g. Publish/ClickOnce and code that refers to it) OR
host your own site (you simply need the static files that Publish process outputs, refer to ClickOnce tutorials for more info).

## Protocol Notes ##

Refer to `docs.htm` file on protocol structure and operation.

## License ##

AvChat client is licensed under MIT License.
