# kevr

**Possibilities for Music VR**


we've made a test build [here](https://github.com/touchtechio/kevr/releases)

0. unzip it.
1. run the exe.
2. steamvr launches and something comes up in your headset. 




for the phone/ipad interface they'll be some more steps:

0. download "touchosc" to your device via the app store. it's maybe $5
1. download the touchosc editor: [TouchOsc Editor](https://hexler.net/software/touchosc)
2. install/launch the editor
3. load the **touchosc** file included in this repo under [tools](https://github.com/touchtechio/kevr/tree/master/tools). this is a layout for the ten finger and zoning interface as well as the correct message addresses for our vr scene
4. "synch" the touch osc layout to your phone. this is a bit trickier maybe. 
5. firstly, it involves having your phone and device on the same wifi.
6. next click "sync" in the editor on your mac.
7. you must open the app on your device/ipad. within the app you can select to get new layouts from the editor. sometimes your mac will just show up along with the layout we made.
8. if not, you must enter the ip address of your machine: [find my mac ip](http://osxdaily.com/2010/11/21/find-ip-address-mac/)
9. nxt you'll need the ip of your steamvr rig and connect your touchosc app to that by typing it in for your OSC>Host as well.
10. from there launch the app and it should just work... 