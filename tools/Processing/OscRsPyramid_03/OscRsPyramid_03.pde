import oscP5.*;
import netP5.*;

OscP5 oscP5, oscLocal;

NetAddress unity;

int cursorX = 200;
int cursorY = 200;
int cursorZ = 50;


PImage dot;


int val;        // Data received from the serial port
OPC opc;
PImage dot1, dot2;
void setup() 
{
  size(600, 100);
  frameRate(30);
//load image

  imageMode(CENTER);

  dot = loadImage("stripes.png");

  oscLocal = new OscP5(this, 12000);

  unity = new NetAddress("127.0.0.1", 8000);

// open multicast
//  oscP5 = new OscP5(this, "127.0.0.1", 8005);
     oscP5 = new OscP5(this, "239.255.0.85", 10085);

  
  // Connect to the local instance of fcserver. You can change this line to connect to another computer's fcserver
  opc = new OPC(this, "127.0.0.1", 7890);


opc.ledStrip(1, 34, width/2, height/2, 10, 0, true);

opc.ledStrip(35, 30, width/2, 5, 10, 0, true);

opc.ledStrip(65, 34, width/2, height/2+10, 10, 0, true);


  // Map an 8x8 grid of LEDs to the center of the window, scaled to take up most of the space
 // float spacing = height / 20.0;
 // opc.ledGrid8x8(0, width/2, height/2, spacing, HALF_PI, false);
  
 // opc.ledGrid(index, 8, 8, x, y, spacing, spacing, angle, zigzag);
//  opc.ledGrid(64, 7, 6, width*2/3, height/2, spacing, spacing, 0, true);
  
 // opc.ledGrid(0, 7, 6, width/3, height/2, spacing, -1*spacing, PI, true);

  
}

float px = 0;
float py = 0;

float pz = 0;

void draw()
{
  background(0);
  //blendMode(ADD);
  
  // Smooth out the mouse location
  px += (cursorX - px) * 0.5;
  py += (cursorY - py) * 0.5;
  pz += (cursorZ - pz) * 0.5;
   

  float dx = px;
  float dy = pz;
  
  // Draw it centered at the mouse location
  image(dot, width-dx+20, height/2, pz, pz);

  fill(0,0,0);
  rect(0,0,width,10);
}


boolean mouseOverRect() { // Test if mouse is over square
  return ((mouseX >= 50) && (mouseX <= 150) && (mouseY >= 50) && (mouseY <= 150));
}


void oscEvent(OscMessage msg) {

  oscLocal.send(msg, unity);
  
  if (msg.addrPattern().contains("heart")) {
    return;
  }

  if (msg.addrPattern().startsWith("/cursor")) {
    
    float x = msg.get(0).floatValue();
    float y = msg.get(1).floatValue();
    float z = msg.get(2).floatValue();
    
    
    cursorX = (int) map (x, 0.7, -0.7, 0, width); 
    cursorY = (int) map (y, -0.5, 0.5, height/4, height*3/4);
    cursorZ = (int) map (z, 0.17, 1.0, 0, 400);

    //cursorZ = (int) map (z, 0.2, 1.0, 0, 255);
   // println( cursorX + "," + cursorY + "," + cursorZ + " : " + z);
   //     println( x + ", " + y + ", " + z + " : " + z);

}
  


}


int mode = 0;
int maxMode = 3;
int nextMode() {
  mode++;
  if (mode > maxMode) { mode = 0; }
  return mode;
}