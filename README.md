# WetImages - Add watermarks to your images

## Usage:

Pass the image file name as an argument in the command line of the executable, or simply create a shortcut of the executable on your desktop and drag the images onto it.

Enjoi.

## Settings

To modify the settings, edit the configuration file wetimages.xml.

The resulting image with the watermark will have its height or width limited by the MaxWidth and/or MaxHeight tag.
>	<MaxWidth>1024</MaxWidth>
>	<MaxHeight>768</MaxHeight>

The watermark must be a 'png' file, and if a full path is not provided, the program will assume it is located in the same folder as the executable.
>	<WatermarkFullPath>watermark.png</WatermarkFullPath>

The watermark scale in relation to the image size, where 0.2 corresponds to 20% of the image size.
>	<WatermarkScale>0.2</WatermarkScale>

The name of the folder that will be used or created to save the image with the watermark, based on the provided image path.
>	<OutputFolder>ComLogo</OutputFolder>

## Meta

Dante Souto – [@dantesouto](https://twitter.com/dantesouto)

Distributed under the MIT license. See ``LICENSE`` for more information.

[https://github.com/DanteSouto/](https://github.com/DanteSouto/)
