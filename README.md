# CoreDC

The project is to produce a well documented and simple to use library implementing the Direct Connect p2p protocol. Mainly in order to simplify the process of writing programs to communicate over the DC protocol.
Our first beta release is out already, but lack of feedback has made us hold back a bit on a more stable release. However, you can always download the latest source from the svn and compile your own, fully working version. Some things are not yet implemented, but basic functionality works.
CoreDC currently features simple methods to:

Connect / disconnect to a specified DC hub.
Send / recieve raw hub messages.
S/r mainchat messages.
S/r private messages.
S/r passive searches.
Recieve passive searchresults.

It automatically handles login procedure and tcp communications and works with all major hubsofts (not tested with all minor ones yet). 
CoreDC also provides a simple, well commented, event-driven interface to work against. A simple GUI project is also provided as an example on how to get started. Writing your first bot requires no more than a little drag and drop and a few simple lines of code, that's it.

