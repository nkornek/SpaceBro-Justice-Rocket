// CameraAspectRatio
// By Nicolas Varchavsky @ Interatica (www.interatica.com)
// Date: March 27th, 2009
// Version: 1.0

// Attach this script to all the cameras you want to modify the aspect ratio.


// here we setup the targeted aspect ratio for width and height
var targetAspectWidth : float = 9.0;
var targetAspectHeight : float = 16.0;

function Awake () 
{
	// we'll try to calculate what's the biggest resolution we can accomplish
	// to work in the desired aspect ratio
	
	// get screen size
	var sw : float = Screen.width;
	var sh : float = Screen.height;
	
	// let's check if we should modify the height first...
	// calculate the targeted size height
	var th : float = sw * (targetAspectHeight / targetAspectWidth);
	
	// these variables will hold the percentage of height or width we need to
	// apply to the camera.rect property
	// by default, we set them up in 1.0
	var ptw : float = 1.0;
	var pth : float = 1.0;
	
	// these variables will help us adjust the margin to center the screen
	var tx : float = 0.0;
	var ty : float = 0.0;
	var half : float = 0.0;
	
	// let's try the height...
	// to do this, we check how much the targeted height represents on the screen height
	// so, if the result is greater than one, it means the height should not be modified since
	// the width is the one needing to be adjusted
	pth = th / sh;
	
	// check if either the height or the width needs to be adjusted
	if (pth > 1.0)
	{
		// since the result was greater than 1.0, we'll work on the width
		// we do the same thing as above, but with the width
		tw = sh * (targetAspectWidth / targetAspectHeight);
		ptw = tw / sw;
		
		// get half of the percentage we're taking from the width
		half = (1.0 - ptw) / 2.0;
		
		// adjust the margin
		tx = half;
		
	}
	else
	{
		// get half of the percentage we're taking from the height
		half = (1.0 - pth) / 2.0;
		
		// adjust the margin
		ty = half;
	}
	
	
	// apply the camera.rect	
	var r : Rect;
	r.x = tx;
	r.y = ty;
	r.width = ptw;
	r.height = pth;
	camera.rect = r;
}

// we require a Camera on this!
@script RequireComponent(Camera)


