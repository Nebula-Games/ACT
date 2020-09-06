using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using ACT.Core.Interfaces.Security.Encryption;
using System.Security;
using System.IO;
using ACT.Core.Extensions;

namespace ACT.Core.Plugins.Security.Encryption
{
    public class ACTStrongEncryption : I_Encryption
    {
        #region TypedCodeFromWeb
        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = System.Text.Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        public string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = System.Security.Cryptography.SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        #endregion


        internal protected static string[] BaseFileData = new string[] { "yFp^Aq%2?Dc7n*m&YW$TkzFFzw2D%&YpQMaB+QdmPF4rc3BDuhw886ffJ4b5Kb@NqCvNF6rNPgCwGFRN$&x_ahc__=S@%?TH!9&2#CGP^$B#skKjxe2j52#gdrY66GJz6YAjJ=vyQE%?hsFmjf7ET5z=6x3M&u^Yfhah_DyPJY?n#?^=#f2Sd77w?E3Wg=taJyLm+VeG%kFh5ef@ZNq5m*Kqm3z4#QW%z7Uqd&5#R5-sNDt@9+XJq@4R&Fz%Mx6M-q_!p%4BP9?cBJ&vxMPa&6+t$dm=!f$cQXZJ8SAdJh_QFPg2mFEvfpFgRc@T+VsEgUcrPUU*x_5_djaAp#^2wrW@&=kmwJXGjz@2Rap!?29!2@m6xLFGc8985ds8*+-^yKvBvJMktcVekvAwgH&W7nJeCt=aKD9Kc_CakYA?jC8&bmRUMesp$G@JT+RztkhVAqmR_bC@j8GBPBxY-rnN$tx6xBNrW=gCbpmczKmRbJ_YGsa6kD%H!n%p_FEtyPph"
       ,"v$6rn5?2WtjNn$AZVk3%SEMGh8myEfQ-tMwg&gE9cM*pVHX^FM?S=8B#QHh+gtDTE!zNLv965Qk6NtzS_2a5fj=@%cE@s$kvFR?WR?D^qFVwh4!mBS8K!8ecn2Nte=qGrP58!!RUgh@SfeNG8MMH*?s2Sk&TvXR9-RbzU!B!zc@5m9t5EHq*^ne@_F9H^cXwvH*Nbq%&fBE#DuSPsK#cb@FX39Hmr=w9CtE4!Yb6h9P8gMr-mPZB?bsP#6^wj_5^dgz9R&M@xhb7jV^%aTBUtehbE%DUutW@yPN9LFx$Y*GYB8^UxBHUd+$VZbNPPNJ#4KAyJ*-@jd?jMJgTvL!M^b!AgM6dPffJ6NnRmcJ8nXRG&%UQYMhuEEm%7&M!8KDu@9X?jKFhL7_b!h2HBWT_%DvHz$yrpg^c&bNR_HD=R6zP9yTw_MuAvWV6NY6gD6CNnzg7MQL78zGKg^nP?GskEtg^GjhWCWgEA$cqJ9%56^xzv_ckc@XY=24a%k25_G32"
       ,"g&Hjvv*%9mZEw6Lw=*cs$NAEAeT+mPKfCjMcT%25eHq8-n2-ZeTEBSGzbV!&@BF5MkC387ddQ=H+5y=v6Tuc5V-*Y=@*T9t4#-&hp7YtWdV9j9ap+LQKe&cYxZR?=jDJnX$$VZ72SXLpn_9U=wb-cnrpRaw!CU9S=C*xYDymJqm$NJSy_hAL!ny&9Z!!*h%tuL72$d^c=7f-%X^vX6fuyeH9+YGT-9N*B^DFpvUj5TcLncW!a9E4*VN?3AVJnBJ%g&mZ!Zd5dXp_n?BusBPkVJZ*7fTXUHwnSst#bqVE?38@uz8$gTB6yA=L=yguHUy3?!HZKD_NrJed%#mr9UzF*&d3k=CxVESZqdrxZwmBukyu!dT!+JZD!s+4Q+Bmz#ftxq--Ty$AxptMneE4YH9HcB4vf$mnt@_AHZ7CpVKgJ*vREkt28T5=n@u_v%Z%!nbfF#w2Q6*_dUS*pFsz7jndT7Qr5C?Aq$tnw7mkxbcK87_u8fTuab3te@aHs!uM2j8w"
       ,"@$8P=^=&g3Z!g#uKb*YUDa3z4r6vgdhRg&EpqdR?^ytJp?SJn%$SPkUBe5zJPhmqtqs3$7V=-Z5Dp&NrNHvndT4xJ^e?fpbCb?azZG#c9^pwjjqGzj-3h_KZufX5$P4c3L-QKN+hqsU7A$nBD5L3+cg8DC7nB-K4Ws=KF753N#jA$hzke+wB4BQH?Cthrhmuz6Cz*2CAkD$xDdJZvkAVPFd+A2Z%AtGTzu__3sa5cVmDCEHdm!&*9*@XXSzk!Kp-M3ysqqzqpb&4RZ!QGCrma?K+JfvHsTsfkH#xJnkfJyQmy3kv9W8tqL7K4LQ!tK%YCPnzj5qu^rxa@w4ak9QH?kGGw5W-8azF@#kLwaf6#GQTXeC6a8ZsfH9SpZ5kFXcN6DE9bWUB-Ag@tuPJKt^j?sVK9uGCqmqZrZkf%tNfWC@V9$ywRAsv_b@Fw@hJ4UEYmhvUJT9wEJ?!J7Dh2*bw+?x-zA@tqsfn%%P38@h@4XVKR2SA#z6&4ercCUdWZrhS"
       ,"X$W6VcqUa_pa8DbjmrW4kXb#EkhS&@R$jC&r+g_rj+Uh$G^S6YcvL89pTYZ4YDXgYj8=DkQBw^y@C^3H2XY$Uaehy6GEzWZ!kwD8W??=LKVq#_aYQDtR#74z?6AxRsfCR8eFfc-Gva@$GWYydu=z?tVpktkK%y7TdP7!VJDqc_b$*+BuZGf=FSK=&2#zSeh&vxvFNq6yg@Kpmueb9a3YH?Ua=H?7myGs$s9kR+pZzb$jR3=VEK^3tFWzAp2Ct2YV=gAU&P6NtfzVkbykSsDnmy6Fkv22GmA6k6PB%DS2BBMd7=U^hrwScyLDmq347WB?&HA=BW2R-R#3#kCvbAdbWpkJ2^LuLHT#n^E3?=g_Ly?BS^!=-2eQh?h5-T?c?y*AA!th5-Pp2NNagY#*PhL*N$HWDse?q$R34=uBprss^5Fwh-SrJmLyUGGV#2mP!xmZE5=P93K*D4?e#5HKfqN87hHdskmY@PycBHD*Ep94%vFtpY?B!$aEz2qKbbJP3vgz"
       ,"Qv^%v_tQVQkbeFc^7w5*VSA?-uWb#uP_QfWJfJQ-ZPfq?b2=#!m5JQSGwFzS=zaRE7eSZChn7H+uynug-Hq954Kvv=MvSu%69kK5f-ADU8eb_DNQ*fRD+f6GksE?#P3Vt^KRu=A?*KA^M8q9qX$=mc#23F$wYTF+upZJjn#YSub&4b_8PMEQdVte4n+_N?DDhvX3D5cf%$L8_a%CHt=Wqu6n_LwsnwtCaJJn64z2s+%NJH-wvuJBNUzacy-hN_NhEtrL=6qyJ5GPhc8atdEa3W-%?xD-!gh&@vwH+7Rdh7Rq3GA^h-wGFfD+br$g7FmzuqqJyK5rH=Z%yE6_d$+#VmPw4BdPn5ne%X7F=yTALzKx$q5$#2LSEQGA_y4hMJFCSVT9TPbXDcDE+sdzuq3CR8T@dR^qBeuSy_aU^gJWT=yz@_^tBMmWP^M!4977tWhpU_sDh6Wp54L-Y&8mZDWrv%8R^BMsjuF?$j@m%wxBH%c6JN8g!gnvFHudgjEUce?H"
       ,"qzH8-GtHZx5y+bZNbSxcYs?Pd6MDqE*uJ2renjmbVp4QdJ46pe@XdqhdLkuT25_tVSmqmy7aCUhKYWX$9M5enV_dhkWtLv4GTs5Q?@sgAu-W?Gsc&6#AU26nWpagfyq+sELLgrz!E34uYAcYYgfkx=JMnueH5-PP?RsJR#jG$dr4-QD86q=Rchu@d$AnZe7qydBDn!cDS&KKNFC$Fz^T%_^vu2=S+@GSQj*LUKQLYNAr_PXqArQ@*2fc34sBEw=UhZshMHndd7EWX58pR^_nMx8*AukKRSU-QEmPMR2=&2S65AFU^QRB4-*&4dCLh#V7S&esL=DxcE4QNjJE_T$5*hp9FqHm8KYPWCz@KY4myTPM*F!m2QYBkeqc&K*h9*U6-^cVX-Cu&8TCyD=6m&6jphs4nPwjP+G#4d+ugw6W^6HrVPBVmKS-dSan&*=GbXhRYaw9&*Gfe=y3bgzZEA&nUG5^aWfw5a9GTSw*Sea^f2e4y%QF?j2y5b-F=zN87d#a"
       ,"5+p+cPp*mMmDfMVSpcpT7CjAPjfQNd6W?zW$Tq+KLPXWJ#YtV!tGz#$g_5_vJ%uzt3SHvdwt5!6?uA&&Jv4_hF2$@-Jcp#HS7ybXH5QdG*@&NVcAKUWTsUakzpqh!*&k-fBe$kM+QUtwt75TC^erb6dZKjse-=wSmMs&ZLYWzEN#355m^A^@VATB?N##Z&4sSVr2ngT#hsNYpTuXY4ADMxc9EL7MRF+s^VcgrWgELYfFMLu=4_8M=tL6r_7UNcuHcp8aju%!S5J3Ae?cUZ935FN9D7tJ&2A8%WN!uDL-eHkB$^vd8g-eZ?55cN8&7PM?Zu7KQ4AkGB?GmNwbcNZPRQU9w#C_HNw$tN3Q_a#b$Wjmt7wkXY6E3KgrPux?qBeZL5MVmLMK=prbgUw8h+k72NS@H=#T7TDXj7PehedG_x$2h#Lt!@ELDt#Myz?A+=M=M34@RUD_FUY-#V_!h*rpkLveQtKDq2A85hkCseTgQznK++r6mJve3GhR+Kjygj5J"
       ,"DRz-3#t#98^ZwXM@C_HPhXGJLeBSK*rFNF7xhWP%6_kFwtLpA+d_@bYh2cr^SJ3ez-m+j2g=wL#9FfHTTJ@6@Pecz!$9BB34+Rc#!&c@DjLDM_wXRQRUhF_J^knGw+ZVYv*A2_?8Vt#E*4L+-d*A*Sb@q=waKP_99jYs5+5U%Btp8NA_qd43rvumY-^_Zg!dA7N5Ca8J7ZZ#G63rpTV=Te+kur2K3Wjy^8cy4CrcRacNyWMfSnxRApYP2BhR9vjB4w2D?sG_#@W-UpqcePsNj^C*752=svcTs+Z2&S^A!Ph=&Q6MP3QE3&_nMf86hPdw@+-S_sKs73L7cKVpQytacAWT$CW5LeP&bUs5L+^93KGcVasKgdFmFtp#z?aS2_P2!b?h44LHvf=ApUFATJc2nB4kt86L?VN$z!Ve%+6w=G^a_uKunf5%AwwZj%m%gh9wuECc!5VA#H$9^ehhNcQb8LE9HfHRN%UvqCxJhfy%=NcbnBj!6CxRxQtY@mt!@9bn"
       ,"Mb2AC&nhQYsu?_7cCd53bGh89qFYvY8d@PWjFN-_Nd#&GJ6QCkxm@wmD7p5CMSVkwWFRYYfyxh^Gq@Jf2TN%?Y&cchENK6E=zxpBU&sE-n++RWgJ&A8vqz4=y-rvVzDAWVh$$pW@De6nqde-GT?DMbm_QCm#DmgDWT%3MWsq-R&yd$Qsbq3^2nmABA*_aq%87z$$6PT$CDTX6zdg6@P*xB%cG^&CgjtW*U2yAQ5TYyWaL+#3F!KuVwne2KLWS5&=xEdDB4zG%b?Dt3gBL*Cy#^+#Mu3D$9qVCLqaD=FB7uQdg297_A!gxy%=DzmCeuPgMFvE^TQC^EZ65+*XJGtgja_Av&u5#Uv#s2DntWn4brhvk?VMu9CM@Q942n6$cTFzSuxD&7dE*65qV*qUde-HfXgcM3J__^cBu#gmhw%s#Z8N+c2pYEXTJRW!&GSRpZ2h%@yvMed5a*XhG!zae^^WU*wnTAba@@YXx_UaVSQhAkxF?ap86-ASH&+RR!FETcU$"
       ,"Az@wT#j=jxcMk74=YqKyxsWkb28qzxFujeEB9fZ_Gabf=nV&x=$+5fRFhP%FpCEp&*-^am=7-w^Lu$WLPYmPs4WA@mn+UJKTsGC6bSp^vSV-k!#LVWJYrSwh9HKST$fp_5@#7V&d^kcKn=-3wPZy4__pyvu#?=8As$7fkWDGV#kjnHgAG#Tp254Dt=F7^bUZ?xQZ_n#%_P*2K3RJX@u-bEU4=keBGUM6=pKwd_5P_UKrPU^^bc^ZPRFcHSrybWM3_n5FKW6ur%bS@wGjC=t#&_nDnG!g-&aTzwfFT6wBpEU@s+CnVaSZxfjqpQ%BY+9f_X%D&UQ@=-Dp557pB%!X!FdW*&QXBvDFMCKEn$?J2v_YkKPkQG5LXJ4Xu%g9tb?LU#%!gjNXv=qjF48qsZHSX--y*@NhEua+-Rqmu-vM=LDLxBk5Ee#ERDLf!4xF8jQGNA@n?u&fxt7+WC#+zA5%BSs*g^6Mfz=uLMjzVMbce2aZ^?BKxyP%Fn=auM2gE?Vn"
       ,"3RdywnR@b2_n_hu2%qhxnrFX%4^hAy!sd7GLmT6D8M?7Q3zp&ugx@uZd+ZtpukTxSPDn-!ua#k9NVMuw93!zr4mMhb^9$!$m&nUC5vjmMSd2UeZbpzMWqEXjRxnw-BG%hj?y+ECSdVmc-_SUcvYD5H9TZ^9Kp&Fm+gYD9=3ETN!B&Zg*%xH8w6+pvfDDDQd+Ht&J_AQeq8MD?GV=*hmPgXd$!gVqS=^$%YBGS9x3CvA#n*p+L?tE!%S8byxK4D4V5xMb+h*34Mg6%Q_t_TBvpS4aTnK2K8Cbg7S=4DaPK$tLmR3u?SG7meaHBx@?%yTQGC9rqX76SKB8#uNx3wS#*Y3?XZs&&Q8aWP2%Uh4?a2^?%ShG4B-V*%t3v7StAg8gnLsbw9h_5UTY!t!%EJ=R5gNyNQEFwyZgrM9sVtCqXpRF!fp!k7azY$^%-MrYGHMfsL6AtK-sNMX&^#SXRb!B3HeF!2eXgN%NHhTtvhxv$Df+h?^PEmvv?asJa-K8p^!d"};

        private const ushort ITERATIONS = 1300;
        private byte[] IV;

        public ACTStrongEncryption(byte[] InitIV)
        {
            IV = InitIV;
        }

        public string Encrypt(string ClearText)
        {
            string iv = string.Empty.PadLeft(32, '#');

            return ACT.Core.Plugins.Security.Encryption.Effortless.Strings.Encrypt(ClearText,BaseFileData[0], BaseFileData[1], iv, Effortless.Bytes.KeySize.Size256, ITERATIONS);
        }

        public string Decrypt(string ClearText)
        {
            string iv = string.Empty.PadLeft(32, '#');

            return ACT.Core.Plugins.Security.Encryption.Effortless.Strings.Decrypt(ClearText, BaseFileData[0], BaseFileData[1], iv, Effortless.Bytes.KeySize.Size256, ITERATIONS);
        }

        public string Encrypt(string clearText, string Password)
        {
            string iv = string.Empty.PadLeft(32, '#');

            return ACT.Core.Plugins.Security.Encryption.Effortless.Strings.Encrypt(clearText, Password, BaseFileData[1], iv, Effortless.Bytes.KeySize.Size256, ITERATIONS);     
        }
        public string Decrypt(string cipherText, string Password)
        {
            string iv = string.Empty.PadLeft(32, '#');

            return ACT.Core.Plugins.Security.Encryption.Effortless.Strings.Decrypt(cipherText, Password, BaseFileData[1], iv, Effortless.Bytes.KeySize.Size256, ITERATIONS);     
        }

        public byte[] Encrypt(byte[] clearData, string Password)
        {
            byte[] key = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.GenerateKey(Password, BaseFileData[1], Effortless.Bytes.KeySize.Size256, ITERATIONS);

            byte[] encrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Encrypt(clearData, key, IV);
            return encrypted;
        }
        public byte[] Decrypt(byte[] cipherData, string Password)
        {
            byte[] key = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.GenerateKey(Password, BaseFileData[1], Effortless.Bytes.KeySize.Size256, ITERATIONS);

            byte[] decrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Decrypt(cipherData, key, IV);

            return decrypted;
        }



        public void Encrypt(string fileIn, string fileOut, string Password)
        {
            System.IO.File.WriteAllBytes(fileOut, Encrypt(System.IO.File.ReadAllBytes(fileIn), Password));
        }
        public void Decrypt(string fileIn, string fileOut, string Password)
        {
            System.IO.File.WriteAllBytes(fileOut, Decrypt(System.IO.File.ReadAllBytes(fileIn), Password));
        }

        /// <summary>
        /// Encrypts the Byte Array using 256 AES Encryption
        /// </summary>
        /// <param name="clearData">Byte Array of Clear Data</param>
        /// <param name="Salt">Minimun 20 Character Salt</param>
        /// <param name="IV">Init Vector - Null Uses Constructor Vector</param>
        /// <param name="Password">File Password - If Blank Uses Internal</param>
        /// <returns>Encrypted Byte Array</returns>
        public byte[] Encrypt(byte[] clearData, string Salt, byte[] IV = null, string Password = "")
        {
            ///GENERATE A KEY BASED ON SALT AND PASSWORD
            byte[] key = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.GenerateKey(Password, Salt , Effortless.Bytes.KeySize.Size256, ITERATIONS);

            if (IV == null)
            {
                byte[] encrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Encrypt(clearData, key, this.IV);
                return encrypted;
            }
            else
            {
                byte[] encrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Encrypt(clearData, key, IV);
                return encrypted;
            }
        }

        /// <summary>
        /// Decrypts the Byte Array using 256 AES Encryption
        /// </summary>
        /// <param name="clearData">Byte Array of Clear Data</param>
        /// <param name="Salt">Minimun 20 Character Salt</param>
        /// <param name="IV">Init Vector - Null Uses Constructor Vector</param>
        /// <param name="Password">File Password - If Blank Uses Internal</param>
        /// <returns>DeCrypted Byte Array</returns>
        public byte[] Decrypt(byte[] cipherData, string Salt, byte[] IV = null, string Password = "")
        {
            ///GENERATE A KEY BASED ON SALT AND PASSWORD
            byte[] key = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.GenerateKey(Password, Salt, Effortless.Bytes.KeySize.Size256, ITERATIONS);

            if (IV == null)
            {
                byte[] decrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Decrypt(cipherData, key, this.IV);
                return decrypted;
            }
            else
            {
                byte[] decrypted = ACT.Core.Plugins.Security.Encryption.Effortless.Bytes.Decrypt(cipherData, key, IV);
                return decrypted;
            }
        }




        //
      

        public string MD5(string value)
        {
            return ACT.Core.Plugins.Security.Encryption.Effortless.Hash.Create(ACT.Core.Plugins.Security.Encryption.Effortless.HashType.MD5, value, BaseFileData[4], false);
        }

        public string MD5ALT(string value)
        {
            throw new NotImplementedException();
        }

        public bool HealthCheck()
        {
            throw new NotImplementedException();
        }


        public string SHA256(string value)
        {
            return ACT.Core.Plugins.Security.Encryption.Effortless.Hash.Create(ACT.Core.Plugins.Security.Encryption.Effortless.HashType.SHA256, value, BaseFileData[4], false);
        }

        public string SHA512(string value)
        {
            return ACT.Core.Plugins.Security.Encryption.Effortless.Hash.Create(ACT.Core.Plugins.Security.Encryption.Effortless.HashType.SHA512, value, BaseFileData[4], false);
        }
    }
}
