MassTorrentEdit
The Multiple Torrent editor for Multitracker torrents.
Version 0.6rc
http://sourceforge.net/projects/bnbtusermods
------------------------------------------------------
This program is distributed under the GPL license.
If you did not recieve a copy of the GPL with the package, if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

The original source project (including precompiled binary AND source) included this in the gpl.txt file in the archive.
------------------------------------------------------
System Requirements:
Visual Basic Runtimes
Python 2.3 installed to c:\python23\ (by source default, can be changed by recompile) - http://www.python.org/
Shad0w Experimental/BitTornado Experimental source package extracted to c:\btsource\ (by source default, can be changed by recompile) - http://www.bittornado.com

------------------------------------------------------
Installation:
Extract this archive to a folder after installing the appropriate runtimes.

------------------------------------------------------
Usage:
Make a torrent for conversion to multitracker load balanced.
For each tracker you want to add, put a new folder in the same folder as the source torrent with a number for the tracker (max of 10)
Copy the torrent into each of the new folders. (not needed after Version 0.6rc)
Launch MassTorrentEdit.exe from the bin folder of the extracted archive
Browse to the torrent you want load balanced.
Select the number of trackers you want to have (max of 10 as of this release)
Enter the announce URLs of each of the trackers.
Enter the tracker hub announce url if desired.
Click "Make Torrent Batch"
It should pop up with one python window for each torrent it needs to change in sequence, plus an additional one for the tracker hub.

------------------------------------------------------
Version History:
Version 0.6rc
- Changed requirements for file preparation.
	Torrent files will be copied immediately before editing instead of needing to be copied by the user.
	This was done to reduce user preparation time.
Version 0.5rc
- First Public build

------------------------------------------------------
Planned features:
- Saving of trackers from session to session.
- Customizable locations for python and the bt source package by configuration file.

------------------------------------------------------
Credits:
- Harold Feit "DreadWingKnight" - GUI, Tracker announce URL cycle routines, tracker announce change call code.
- John Hoffman "TheSHAD0W" - Torrent Announce change code used in this project, one hell of a BT client.
- Python Software Foundation - Python Runtimes.
