# BitalinoGui -  Project in which I worked during my erasmmus placement 
A  project for recording the process of multiple IOT devices.The main idea is to detect emotion responses when a subject is hearing music.
Devices that project depends on and capabilities:

1)Bitalino Sensor physioological signlas collection

2)Intel Real Sense capture depth video

3)Common joystick for annotating the data based on this model 
->(https://github.com/kostasAnton/BitalinoGui/blob/master/BitalinoGui/Resources/Two-dimensional-valence-arousal-space.png)

4)Start songs on a  background thread as a playlist.Between each song there is 45sec break  so the subject can calm down

5)Synchronize data using midllewear of labrecorder.Lab recorder gives a struct as a result which contains the relations between the data.
lab recorder->https://github.com/sccn/labstreaminglayer/wiki

To  set this up:

1.You  need a bitalino sensor kit the ble edition.

2.Lab recorder  on the background.Start the middlewear(you will see a graphical user interface)

3.Launch bitalino gui app, start the recording process by choosing devices you want to record by the corresponding list boxes.

4.Go to lab recorder and check the devices in lab recorder you want to capture and synchronize frames

5.Make sure that the led light sensor is connected to bitalino.Now you can start music on the background using the load button from menu (bitalino gui)

Now all frames are tagged for every song and pushed to lab recorder for sync.At the end you can also export a txt file with the data from  bitalino Gui.


