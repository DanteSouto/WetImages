# WetImages - Add watermarks to your images

## Usage:

Pass the image file name as an argument in the command line of the executable, or simply create a shortcut of the executable on your desktop and drag the images onto it.

Enjoi.

## Settings

To modify the settings, edit the configuration file wetimages.xml.

The resulting image with the watermark will have its height or width limited by the MaxWidth and/or MaxHeight tag.
>	&lt;MaxWidth&gt;1024&lt;/MaxWidth&gt;
>	&lt;MaxHeight&gt;768&lt;/MaxHeight&gt;

The watermark must be a 'png' file, and if a full path is not provided, the program will assume it is located in the same folder as the executable.
>	&lt;WatermarkFullPath&gt;watermark.png&lt;/WatermarkFullPath&gt;

The watermark scale in relation to the image size, where 0.2 corresponds to 20% of the image size.
>	&lt;WatermarkScale&gt;0.2&lt;/WatermarkScale&gt;

The name of the folder that will be used or created to save the image with the watermark, based on the provided image path.
>	&lt;OutputFolder&gt;ComLogo&lt;/OutputFolder&gt;

## Meta

Dante Souto – [@dantesouto](https://twitter.com/dantesouto)

Distributed under the MIT license. See ``LICENSE`` for more information.

[https://github.com/DanteSouto/](https://github.com/DanteSouto/)
