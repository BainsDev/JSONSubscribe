<h1 align="center">
  JSONSubscribe
</h1>

<h4 align="center">Subscribes to all JSON API events from the LCU. </center>


![screenshot](https://raw.githubusercontent.com/H-Bains/JSONSubscribe/master/JSONSubscribe/JSONSubscribe.gif)

## Description

The League of Legends client creates an internal webserver upon being launched. 
By obtaining the credentials from the generated lockfile, a connection can be established to the WAMP server to monitor JSON API results. 
This can be used to explore the LCU API and gain a deeper understanding of how the client operates.

## Key Features
  
* JSON Deserialization
  - Deserializes JSON data received from the WAMP OnJsonApiEvent. 
  
* Beautify results
  - Encloses the uri, event type, and data in a block for readability.
  
* Auto Connect
  - Automatically connects to a running LCU instance.

## How To Use

To use this application, make sure you are on a Windows machine with administrator privileges. 
Launch the League of Legends client, then launch JSONSubscribe. 
JSONSubscribe will automatically attach to the running LCU instance.

## Credits

This software uses the following open source packages:

- [WebSocketSharp](https://github.com/sta/websocket-sharp)
- [Fody Costura](https://github.com/Fody/Costura)
- [Json.net](https://github.com/JamesNK/Newtonsoft.Json/)
- [SimpleJson](https://github.com/simplejson/simplejson)

## License

MIT

---
