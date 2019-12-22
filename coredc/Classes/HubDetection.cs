#region License

/* Copyright 2007 © Johan Pettersson, Fredrik Johansson, Michael Starberg, Christian Epstein.
 
This file is part of CoreDC.

CoreDC is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 2 of the License, or
(at your option) any later version.

CoreDC is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with CoreDC.  If not, see <http://www.gnu.org/licenses/>. */

#endregion
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoreDC.Classes
{
    public static class HubDetection
    {
        public enum HubType
        {
            Unknown, Undetected, NMDCHub, PtokaX, SBHub, xHub, Shasta, Netcomandos, YABBA,
            Ryalth, SDCH, DDCH, DCHpp, OpenDCHub, OpenCSharpHub, OpenDCVBHub, NMDCHub2,
            CSharpHub, Verlihub, WinVerlihub, AngelHub, YHub, YnHub, Ash, ImpHub,
            RedirectHub, DCSH, DCSHM, ZPocHub, xDC, ISDCH, LatHack, Nimaci, DCHPro,
            SDHub, Sparrow, TMMDCHub, GSDCHub, BlackDCHub, DCGalaxy, HexHub, AquilaHub,
            TkHub, AdmiHub, xHub2, OpenDCD, DCDaemon, NitroHub
        }

        static readonly List<KeyValuePair<string, HubType>> hubTypes;
        
        // Regexp originally written by Gadget, posted on the DCpp blog, assumed to be public domain.
        static HubDetection()
        {
            hubTypes = new List<KeyValuePair<string, HubType>>();
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL::This_hub_was_written_by_Yoshi::.{10,15} Pk=YnHub$", HubType.YnHub)); // YnHub
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z\:\;\<\=\>\?\@\[\\\]\^_\`]{45} Pk=PTOKAX[0-9A-Za-z\:\;\<\=\>\?\@\[\\\]\^_\`]{16}$", HubType.PtokaX)); // Ptaczek's version
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z\:\;\<\=\>\?\@\[\\\]\^_\`]{45} Pk=PTOKAX$", HubType.PtokaX)); //PtokaX DC Hub 0.3.3.0 PPK devel build
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL[0-9A-Za-z\:\;\<\=\>\?\@\[\\\]\^_\`]{29} Pk=PtokaX$", HubType.PtokaX)); //Version 0.3.3.0 build 16.07 or above
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL[0-9A-Za-z\:\;\<\=\>\?\@\[\\\]\^_\`]{30} Pk=PtokaX$", HubType.PtokaX)); //Version 0.3.3.1 or above
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_verlihub (Pk|pK)=version[a-zA-Z0-9_\.\+\- ]{1,60}$", HubType.Verlihub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_(winverlihub|winverliHub|WinVerliHub) (Pk|pK)=version[a-zA-Z0-9_\.\+\- ]{1,60}$", HubType.WinVerlihub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLAquila\[\[.{4}\]\] Pk=Aquila$", HubType.AquilaHub)); //Aquila by Jove
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOL:SPEEDSHAKE_This_hub_was_written_by_Yoshi_ Pk=None", HubType.YHub)); //pre-0.385
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock YHUB::This_hub_was_written_by_Yoshi::.{13,20} Pk=[A-Za-z0-9\.\[\] ]{3,26}$", HubType.YHub)); //pre-Beta 0.387 [t9]
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL::This_hub_was_written_by_Yoshi::.{13,20} Pk=YHub$", HubType.YHub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{40,120} Pk=[0-9A-Za-z\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{16}$", HubType.NMDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock .{30,80} Pk=SBDCHUB$", HubType.SBHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock 987654321 Pk=Xh", HubType.xHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock 987654321 Pk=xx", HubType.xHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOL987654321 Pk=Xh", HubType.xHub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z\*\<\>\%\-\'\:\;\(\)\+]{54} Pk=[0-9A-Za-z\*\<\>\%\-\'\:\;\(\)\+]{16}$", HubType.Shasta));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,80} Pk=[0-9A-Za-y]{10}$", HubType.Netcomandos));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{28} Pk=$", HubType.Netcomandos)); //Looks like it, claims to be NMDCH, lock length static and pk missing
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOL Pk=NETC", HubType.Netcomandos)); //Newer variation
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,60} Pk=\[YABBA-[0-9\.]{5,8}\][0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{2}$", HubType.YABBA)); //Version 1
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock .{50,100} Pk=YABBA-2.x-(win32|linux)$", HubType.YABBA)); //Version 2
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock o=;CxWkTu=59fJ,kPvRe1ysMaL7UxS<agr3@340;,KQfoMZ8FJH-D&I&tY(7Lop0aKQA%(K[zZ?PBz;nPi. Pk='mL@lNXypQ;uE,X)", HubType.Ryalth));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [a-z]{2,200} Pk=[a-z]{2,200}$", HubType.NMDCHub2));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=SDCH[0-9\.\,]{1,9}DEFDEF$", HubType.SDCH));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=DDCH[0-9\.\,]{1,9}DEFDEF$", HubType.DDCH)); //DDCH by TheNOP 0.3.37
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=DDCH[0-9\.\,]{1,9} SVN [0-9]{2,4}DEFDEF$", HubType.DDCH)); //DDCH by TheNOP 0.3.38
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=DDCH[0-9\.\,]{1,9}SVN[0-9]{2,4}DEFDEF$", HubType.DDCH)); //DDCH by TheNOP >=0.3.39
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock NONEXTENDEDPROTOCOLABCABC Pk=DCHUBPLUSPLUS[0-9\.]{1,6}ABCABC$", HubType.DCHpp)); //pre-0.290
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLABCABC Pk=DCHUBPLUSPLUS[0-9a-z\.]{1,7}ABCABC$", HubType.DCHpp));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock Sending_key_isn't_neccessary,_key_won't_be_checked. Pk=Same_goes_here.", HubType.OpenDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABCABCABCABC Pk=OPENDCVBHUBABCABC", HubType.OpenDCVBHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABC Pk=OPENCSHARPHUB", HubType.CSharpHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock NONEXTENDEDPROTOCOLABCABCABCABC Pk=OPENCSHARPHUB", HubType.OpenCSharpHub)); //Older version
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock ANGELHUBSERVEREXTENDEDPROTOCOL pk=ANGELHUBV[0-9\.]{1,6}$", HubType.AngelHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("Lock EXTENDEDPROTOCOLashwaswrittenbynevandyoshiandfusbar2DfsTPlnd Pk=c2DaLnrJQ2dsE('P", HubType.Ash));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock LINUXHUB Pk=EXTENDED", HubType.ImpHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOL Pk=ImpHub", HubType.ImpHub)); //clone5.myftp.org
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z]{16} Pk=ImpHub$", HubType.ImpHub)); //newer version
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock REDIRECTHUB-V[0-9\.]{1,5}-ABCABCABCABCABCABC Pk=aDe2003$", HubType.RedirectHub)); //Ade's version
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock DC++HubRedirecterBySniper Pk=HubRedirecter", HubType.RedirectHub)); //Sniper's version
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX Pk=DIRECTCONNECTSECURHUB", HubType.DCSH)); //pre v1.2
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX Pk=DCSH:[0-9\.]{1,5}$", HubType.DCSH));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLABCABCABCABCABCABC Pk=DCSH:[0-9\.]{1,5}$", HubType.DCSHM));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,100} Pk=Net4God$", HubType.ZPocHub)); //Net2God/OpenDCD variation
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,100} Pk=AllianceHubServer$", HubType.ZPocHub)); //AllianceHubServer/OpenDCD variation
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABCABCABCABC Pk=ZDCROOMSERVERAABC", HubType.ZPocHub)); //ZDC Room Server (ZPOC w/dc protocol)
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABCABCABCABC Pk=ZPOCCHRISITANCABC", HubType.ZPocHub)); //ZDC Room Server (ZPOC w/dc protocol)
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock X(f355e24-3Q6hgfsBd3#3rd2 Pk=PTOKAX%3FTc/", HubType.Verlihub)); //arena.tropico.se, claims to be ptokax but static and too short lock
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock ABCDEFGHIJKLMNOPQABCDEFGHIJKLMNOPQ Pk=THISISATEST", HubType.Undetected)); //dc411.jmimix.no-ip.org "PtokaX DC Hub 0.3.2.2 PF'2003 edition"
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock NoLockImplementedYet Pk=1234", HubType.Undetected)); //borf.mine.nu
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=xDC[0-9\.\,]{1,5}DEFDEF$", HubType.xDC)); //xDC
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=ISDCH[0-9\.\,]{1,5}DEFDEF$", HubType.ISDCH));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOL::LatHack_DC_Hub Pk=LatHack_DC_Hub", HubType.LatHack));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_nimaci Pk=version[0-9\.\,]{1,7}$", HubType.Nimaci));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock DCHPRO Pk=[0-9a-zA-Z\.\,]{5,8}$", HubType.DCHPro));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [A-Z] Pk=[0-9A-Za-z\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{15,16}$", HubType.SDHub)); //SdHub++ 0.10, Agent 0017's hub software (with invalid lock)
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABCABCABCABC Pk=PTOKAX_0.1.6", HubType.Undetected)); //petson.mine.nu, maybe leaked DCH++ or opendcvbhub?
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock Sparrow DC HUB:11241124740527", HubType.Sparrow)); //sparrow.no-ip.biz, only seen once
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{50,100} $", HubType.Undetected)); //dc.buh.cz (claiming to be nmdch 1.0.25, lock missing, looks like opendcd variation)
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLABCABCABCABCABCABCABC Pk=TMMDCHUBABCABC", HubType.TMMDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock BUILDBYCHRISTIANTREPANIER@GSPRODUC.WEBHOP.ORG Pk=GSDCHUBSERVERABABABABA", HubType.GSDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLBLACKDCHUBBLACKDCHUBBLACKDCHUB Pk=BLACKDCHUB_WIN32", HubType.BlackDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>("$Lock EXTENDEDPROTOCOLBLACKDCHUBBLACKDCHUBBLACKDCHUB Pk=BLACKDCHUB_LINUX", HubType.BlackDCHub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL[0-9A-Za-z\.\,\-_]{1,40} Pk=DC_Galaxy$", HubType.DCGalaxy)); //b_w_johan-Kkipars-K-Pone-Shadow
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_HeXhub_RO_[a-zA-Z0-9]{10,16} Pk=versiunea[a-zA-Z0-9\.]{1,8}$", HubType.HexHub)); //Lord_Zero's hubsoft
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_HeXHub_TE_[a-zA-Z0-9]{10,16} Pk=versiunea[a-zA-Z0-9\.]{1,8}$", HubType.HexHub)); //Especially lame TE edition?
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL::TkHub_was_written_by_Cat::[0-9\.]{3,60} Pk=tkhub$", HubType.TkHub)); //TkHub by Cat
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL::AdmiHUB_[0-9\.\,]{1,8} Pk=Irrevelant$", HubType.AdmiHub));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-z\.\,\-_]{3,32} Pk=xhub$", HubType.xHub2));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,100} Pk=OpenDCd$", HubType.OpenDCD));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{30,100} Pk=opendcd$", HubType.OpenDCD));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock [0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{50,100} Pk=[0-9A-Za-y\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^_\`]{5}$", HubType.DCDaemon));
            hubTypes.Add(new KeyValuePair<string, HubType>(@"^\$Lock EXTENDEDPROTOCOL_Hubsoft_made_by_TnT[ [0-255\.] ] Pk=NitroHub", HubType.NitroHub));
        }

        /// <summary>
        /// Detect what hubsoft the hub is running based on the $Lock information
        /// </summary>
        /// <param name="lck">The $Lock recieved from the hub</param>
        /// <returns>The hubtype</returns>
        public static HubType IdentifyHub(string lck)
        {
            // TODO: Most work now, but some are not identified. Probably hubs that are
            // newer than the algorithm and should be added, se list below.
            // Could just be clones of some other hub of course..

            /*
            $Lock EXTENDEDPROTOCOL_Frozen_DC_Hub Pk=FDCH0.711.StellarFoxABCABCABCABC|
            $Lock EXTENDEDPROTOCOL:This_Hub_Was_Written_By_-=FD=-: Pk=Nebula_DC_Hubsoft|
            $Lock EXTENDEDPROTOCOLDEFDEFDEFDEFDEFDEFDEF Pk=ABC1,23DEFDEF|
            $Lock EXTENDEDPROTOCOL_LeSSeL Pk=CowHUB|
            $Lock EXTENDEDPROTOCOL::UFOHub_3.08.7::8Fi^;E Pk=UFOHub_v3.08.7TP|
            $Lock EXTENDEDPROTOCOLmQjkeDvaZSyTH\]_A[fOzVhXoBFuqCEbPNiIwdMLU`psYrx^ Pk=FDCH0.800.A0ABCABCABCABC|
            */

            // Crashes on mono sometimes without try/catch
            try
            {
                foreach (KeyValuePair<string, HubType> pair in hubTypes)
                {
                    if (Regex.IsMatch(lck, pair.Key))
                    {
                        return pair.Value;
                    }
                }
            }
            catch { }

            return HubType.Unknown;
        }
    }
}
