
Option Explicit On 
Option Strict On

Imports System.Text
Imports System.Security.Cryptography

' Namespace added for organization within the class library dll.
Namespace EAD.Cryptography.TigerNET

    Public Class Tiger
        Inherits HashAlgorithm

        Const PASSES As Integer = 3
        Const BLOCKSIZE As Integer = 64

        Dim lLen As Long

        Dim a As Long
        Dim b As Long
        Dim c As Long

        Dim buf As Byte() = New Byte(BLOCKSIZE - 1) {}
        Dim nBufPos As Integer

        Dim block As Long() = New Long(7) {}

        Public Sub New()
            MyBase.New()
            Initialize()
        End Sub

        Public Overrides ReadOnly Property CanTransformMultipleBlocks() As Boolean
            Get
                Return True
            End Get
        End Property

        Protected Overrides Sub HashCore(ByVal data() As Byte, ByVal nStart As Integer, ByVal nSize As Integer)

            Dim nBufPos, nEnd, nToCopy As Integer
            Dim buf As Byte()

            Me.lLen += nSize

            buf = Me.buf
            nBufPos = Me.nBufPos

            nEnd = nStart + nSize

            While nStart < nEnd

                nToCopy = BLOCKSIZE - nBufPos

                If nToCopy > nSize Then
                    nToCopy = nSize
                End If

                Array.Copy(data, nStart, buf, nBufPos, nToCopy)

                nStart += nToCopy
                nBufPos += nToCopy
                nSize -= nToCopy

                If BLOCKSIZE > nBufPos Then
                    Exit While
                End If

                ProcessBlock()

                nBufPos = 0

            End While

            Me.nBufPos = nBufPos

        End Sub

        Sub ProcessBlock()

            Dim nI, nPos As Integer
            Dim l As Long
            Dim block As Long()
            Dim buf As Byte()

            nPos = 0
            block = Me.block
            buf = Me.buf

            While nPos < BLOCKSIZE

                block(nI) = _
                    (CLng(buf(nPos + 7)) << 56) Or _
                    ((CLng(buf(nPos + 6)) And &HFF) << 48) Or _
                    ((CLng(buf(nPos + 5)) And &HFF) << 40) Or _
                    ((CLng(buf(nPos + 4)) And &HFF) << 32) Or _
                    ((CLng(buf(nPos + 3)) And &HFF) << 24) Or _
                    ((CLng(buf(nPos + 2)) And &HFF) << 16) Or _
                    ((CLng(buf(nPos + 1)) And &HFF) << 8) Or _
                    (CLng(buf(nPos)) And &HFF)

                nPos += 8
                nI += 1
            End While

            Compress()

        End Sub

        Sub Compress()

            Dim aa, bb, cc As Long
            Dim block As Long()

            aa = Me.a
            bb = Me.b
            cc = Me.c

            block = Me.block

            RoundABC(block(0), 5)
            RoundBCA(block(1), 5)
            RoundCAB(block(2), 5)
            RoundABC(block(3), 5)
            RoundBCA(block(4), 5)
            RoundCAB(block(5), 5)
            RoundABC(block(6), 5)
            RoundBCA(block(7), 5)

            Schedule(block)

            RoundCAB(block(0), 7)
            RoundABC(block(1), 7)
            RoundBCA(block(2), 7)
            RoundCAB(block(3), 7)
            RoundABC(block(4), 7)
            RoundBCA(block(5), 7)
            RoundCAB(block(6), 7)
            RoundABC(block(7), 7)

            Schedule(block)

            RoundBCA(block(0), 9)
            RoundCAB(block(1), 9)
            RoundABC(block(2), 9)
            RoundBCA(block(3), 9)
            RoundCAB(block(4), 9)
            RoundABC(block(5), 9)
            RoundBCA(block(6), 9)
            RoundCAB(block(7), 9)

            Me.a = Me.a Xor aa
            Me.b -= bb
            Me.c += cc

        End Sub

        Sub Schedule(ByVal x As Long())

            x(0) -= x(7) Xor &HA5A5A5A5A5A5A5A5L
            x(1) = x(1) Xor x(0)
            x(2) += x(1)
            x(3) -= x(2) Xor ((Not x(1)) << 19)
            x(4) = x(4) Xor x(3)
            x(5) += x(4)
            x(6) -= x(5) Xor (((Not x(4)) >> 23) And &H1FFFFFFFFFF)
            x(7) = x(7) Xor x(6)
            x(0) += x(7)
            x(1) -= x(0) Xor ((Not x(7)) << 19)
            x(2) = x(2) Xor x(1)
            x(3) += x(2)
            x(4) -= x(3) Xor (((Not x(2)) >> 23) And &H1FFFFFFFFFF)
            x(5) = x(5) Xor x(4)
            x(6) += x(5)
            x(7) -= x(6) Xor &H123456789ABCDEFL
        End Sub


        Sub RoundABC(ByVal x As Long, ByVal mul As Integer)

            Dim c As Long
            Dim cl, ch As Integer
            Dim T() As Long = Tiger.T

            c = Me.c

            c = c Xor x

            ch = CInt(c >> 32)
            cl = CInt(c)

            Me.a -= T(cl And &HFF) Xor T(((cl >> 16) And &HFF) + 256) Xor T((ch And &HFF) + 512) Xor T(((ch >> 16) And &HFF) + 768)
            Me.b += T(((cl >> 8) And &HFF) + 768) Xor T(((cl >> 24) And &HFF) + 512) Xor T(((ch >> 8) And &HFF) + 256) Xor T((ch >> 24) And &HFF)

            Me.b *= mul

            Me.c = c

        End Sub

        Sub RoundBCA(ByVal x As Long, ByVal mul As Integer)

            Dim a As Long
            Dim al, ah As Integer
            Dim T() As Long = Tiger.T

            a = Me.a

            a = a Xor x

            ah = CInt(a >> 32)
            al = CInt(a)

            Me.b -= T(al And &HFF) Xor T(((al >> 16) And &HFF) + 256) Xor T((ah And &HFF) + 512) Xor T(((ah >> 16) And &HFF) + 768)
            Me.c += T(((al >> 8) And &HFF) + 768) Xor T(((al >> 24) And &HFF) + 512) Xor T(((ah >> 8) And &HFF) + 256) Xor T((ah >> 24) And &HFF)
            Me.c *= mul

            Me.a = a
        End Sub

        Sub RoundCAB(ByVal x As Long, ByVal mul As Integer)

            Dim b As Long
            Dim bl, bh As Integer
            Dim T() As Long = Tiger.T

            b = Me.b

            b = b Xor x

            bh = CInt(b >> 32)
            bl = CInt(b)

            Me.c -= T(bl And &HFF) Xor T(((bl >> 16) And &HFF) + 256) Xor T((bh And &HFF) + 512) Xor T(((bh >> 16) And &HFF) + 768)
            Me.a += T(((bl >> 8) And &HFF) + 768) Xor T(((bl >> 24) And &HFF) + 512) Xor T(((bh >> 8) And &HFF) + 256) Xor T((bh >> 24) And &HFF)
            Me.a *= mul

            Me.b = b
        End Sub

        Private Shared Sub LongToBytes(ByVal lVal As Long, ByVal buf() As Byte, ByVal nIdx As Integer)

            Dim nEnd As Integer = nIdx + 8

            While nIdx < nEnd
                buf(nIdx) = CByte(lVal And &HFF)
                lVal >>= 8
                nIdx += 1
            End While
        End Sub

        Protected Overrides Function HashFinal() As Byte()

            Dim nBufPos, nI As Integer
            Dim lPart As Long
            Dim buf() As Byte
            Dim result() As Byte

            nBufPos = Me.nBufPos
            buf = Me.buf

            buf(nBufPos) = 1

            nBufPos += 1

            If (BLOCKSIZE - 8) <= nBufPos Then

                Array.Clear(buf, nBufPos, BLOCKSIZE - nBufPos)

                ProcessBlock()
                nBufPos = 0
            End If

            Array.Clear(buf, nBufPos, BLOCKSIZE - nBufPos - 8)

            LongToBytes(Me.lLen << 3, buf, BLOCKSIZE - 8)

            ProcessBlock()

            result = New Byte(23) {}

            LongToBytes(Me.a, result, 0)
            LongToBytes(Me.b, result, 8)
            LongToBytes(Me.c, result, 16)

            Return result

        End Function

        Public Overrides Sub Initialize()

            Me.a = &H123456789ABCDEF
            Me.b = &HFEDCBA9876543210
            Me.c = &HF096A5B4C3B2E187

            Me.nBufPos = 0
            Me.lLen = 0
        End Sub

        Public Shared Function SelfTest() As Boolean

            ' (NESSIE test vector)
            Dim TEST_DATA As String = "abcdbcdecdefdefgefghfghighijhijkijkljklmklmnlmnomnopnopq"
            Dim TEST_HASH() As Byte = { _
                &HF, &H7B, &HF9, &HA1, &H9B, &H9C, &H58, &HF2, _
                &HB7, &H61, &HD, &HF7, &HE8, &H4F, &HA, &HC3, _
                &HA7, &H1C, &H63, &H1E, &H7B, &H53, &HF7, &H8E}

            Dim nI As Integer
            Dim tg As Tiger
            Dim hash() As Byte
            Dim enc As ASCIIEncoding

            tg = New Tiger
            tg.Initialize()

            enc = New ASCIIEncoding
            hash = tg.ComputeHash(enc.GetBytes(TEST_DATA))

            If hash.Length <> TEST_HASH.Length Then
                Return False
            End If

            For nI = 0 To TEST_HASH.Length - 1
                If hash(nI) <> TEST_HASH(nI) Then
                    Return False
                End If
            Next

            Return True
        End Function

        Private Shared T As Long() = New Long(4 * 256 - 1) { _
        &H2AAB17CF7E90C5E, &HAC424B03E243A8EC, &H72CD5BE30DD5FCD3, &H6D019B93F6F97F3A, _
        &HCD9978FFD21F9193, &H7573A1C9708029E2, &HB164326B922A83C3, &H46883EEE04915870, _
        &HEAACE3057103ECE6, &HC54169B808A3535C, &H4CE754918DDEC47C, &HAA2F4DFDC0DF40C, _
        &H10B76F18A74DBEFA, &HC6CCB6235AD1AB6A, &H13726121572FE2FF, &H1A488C6F199D921E, _
        &H4BC9F9F4DA0007CA, &H26F5E6F6E85241C7, &H859079DBEA5947B6, &H4F1885C5C99E8C92, _
        &HD78E761EA96F864B, &H8E36428C52B5C17D, &H69CF6827373063C1, &HB607C93D9BB4C56E, _
        &H7D820E760E76B5EA, &H645C9CC6F07FDC42, &HBF38A078243342E0, &H5F6B343C9D2E7D04, _
        &HF2C28AEB600B0EC6, &H6C0ED85F7254BCAC, &H71592281A4DB4FE5, &H1967FA69CE0FED9F, _
        &HFD5293F8B96545DB, &HC879E9D7F2A7600B, &H860248920193194E, &HA4F9533B2D9CC0B3, _
        &H9053836C15957613, &HDB6DCF8AFC357BF1, &H18BEEA7A7A370F57, &H37117CA50B99066, _
        &H6AB30A9774424A35, &HF4E92F02E325249B, &H7739DB07061CCAE1, &HD8F3B49CECA42A05, _
        &HBD56BE3F51382F73, &H45FAED5843B0BB28, &H1C813D5C11BF1F83, &H8AF0E4B6D75FA169, _
        &H33EE18A487AD9999, &H3C26E8EAB1C94410, &HB510102BC0A822F9, &H141EEF310CE6123B, _
        &HFC65B90059DDB154, &HE0158640C5E0E607, &H884E079826C3A3CF, &H930D0D9523C535FD, _
        &H35638D754E9A2B00, &H4085FCCF40469DD5, &HC4B17AD28BE23A4C, &HCAB2F0FC6A3E6A2E, _
        &H2860971A6B943FCD, &H3DDE6EE212E30446, &H6222F32AE01765AE, &H5D550BB5478308FE, _
        &HA9EFA98DA0EDA22A, &HC351A71686C40DA7, &H1105586D9C867C84, &HDCFFEE85FDA22853, _
        &HCCFBD0262C5EEF76, &HBAF294CB8990D201, &HE69464F52AFAD975, &H94B013AFDF133E14, _
        &H6A7D1A32823C958, &H6F95FE5130F61119, &HD92AB34E462C06C0, &HED7BDE33887C71D2, _
        &H79746D6E6518393E, &H5BA419385D713329, &H7C1BA6B948A97564, &H31987C197BFDAC67, _
        &HDE6C23C44B053D02, &H581C49FED002D64D, &HDD474D6338261571, &HAA4546C3E473D062, _
        &H928FCE349455F860, &H48161BBACAAB94D9, &H63912430770E6F68, &H6EC8A5E602C6641C, _
        &H87282515337DDD2B, &H2CDA6B42034B701B, &HB03D37C181CB096D, &HE108438266C71C6F, _
        &H2B3180C7EB51B255, &HDF92B82F96C08BBC, &H5C68C8C0A632F3BA, &H5504CC861C3D0556, _
        &HABBFA4E55FB26B8F, &H41848B0AB3BACEB4, &HB334A273AA445D32, &HBCA696F0A85AD881, _
        &H24F6EC65B528D56C, &HCE1512E90F4524A, &H4E9DD79D5506D35A, &H258905FAC6CE9779, _
        &H2019295B3E109B33, &HF8A9478B73A054CC, &H2924F2F934417EB0, &H3993357D536D1BC4, _
        &H38A81AC21DB6FF8B, &H47C4FBF17D6016BF, &H1E0FAADD7667E3F5, &H7ABCFF62938BEB96, _
        &HA78DAD948FC179C9, &H8F1F98B72911E50D, &H61E48EAE27121A91, &H4D62F7AD31859808, _
        &HECEBA345EF5CEAEB, &HF5CEB25EBC9684CE, &HF633E20CB7F76221, &HA32CDF06AB8293E4, _
        &H985A202CA5EE2CA4, &HCF0B8447CC8A8FB1, &H9F765244979859A3, &HA8D516B1A1240017, _
        &HBD7BA3EBB5DC726, &HE54BCA55B86ADB39, &H1D7A3AFD6C478063, &H519EC608E7669EDD, _
        &HE5715A2D149AA23, &H177D4571848FF194, &HEEB55F3241014C22, &HF5E5CA13A6E2EC2, _
        &H8029927B75F5C361, &HAD139FABC3D6E436, &HD5DF1A94CCF402F, &H3E8BD948BEA5DFC8, _
        &HA5A0D357BD3FF77E, &HA2D12E251F74F645, &H66FD9E525E81A082, &H2E0C90CE7F687A49, _
        &HC2E8BCBEBA973BC5, &H1BCE509745F, &H423777BBE6DAB3D6, &HD1661C7EAEF06EB5, _
        &HA1781F354DAACFD8, &H2D11284A2B16AFFC, &HF1FC4F67FA891D1F, &H73ECC25DCB920ADA, _
        &HAE610C22C2A12651, &H96E0A810D356B78A, &H5A9A381F2FE7870F, &HD5AD62EDE94E5530, _
        &HD225E5E8368D1427, &H65977B70C7AF4631, &H99F889B2DE39D74F, &H233F30BF54E1D143, _
        &H9A9675D3D9A63C97, &H5470554FF334F9A8, &H166ACB744A4F5688, &H70C74CAAB2E4AEAD, _
        &HF0D091646F294D12, &H57B82A89684031D1, &HEFD95A5A61BE0B6B, &H2FBD12E969F2F29A, _
        &H9BD37013FEFF9FE8, &H3F9B0404D6085A06, &H4940C1F3166CFE15, &H9542C4DCDF3DEFB, _
        &HB4C5218385CD5CE3, &HC935B7DC4462A641, &H3417F8A68ED3B63F, &HB80959295B215B40, _
        &HF99CDAEF3B8C8572, &H18C0614F8FCB95D, &H1B14ACCD1A3ACDF3, &H84D471F200BB732D, _
        &HC1A3110E95E8DA16, &H430A7220BF1A82B8, &HB77E090D39DF210E, &H5EF4BD9F3CD05E9D, _
        &H9D4FF6DA7E57A444, &HDA1D60E183D4A5F8, &HB287C38417998E47, &HFE3EDC121BB31886, _
        &HC7FE3CCC980CCBEF, &HE46FB590189BFD03, &H3732FD469A4C57DC, &H7EF700A07CF1AD65, _
        &H59C64468A31D8859, &H762FB0B4D45B61F6, &H155BAED099047718, &H68755E4C3D50BAA6, _
        &HE9214E7F22D8B4DF, &H2ADDBF532EAC95F4, &H32AE3909B4BD0109, &H834DF537B08E3450, _
        &HFA209DA84220728D, &H9E691D9B9EFE23F7, &H446D288C4AE8D7F, &H7B4CC524E169785B, _
        &H21D87F0135CA1385, &HCEBB400F137B8AA5, &H272E2B66580796BE, &H3612264125C2B0DE, _
        &H57702BDAD1EFBB2, &HD4BABB8EACF84BE9, &H91583139641BC67B, &H8BDC2DE08036E024, _
        &H603C8156F49F68ED, &HF7D236F7DBEF5111, &H9727C4598AD21E80, &HA08A0896670A5FD7, _
        &HCB4A8F4309EBA9CB, &H81AF564B0F7036A1, &HC0B99AA778199ABD, &H959F1EC83FC8E952, _
        &H8C505077794A81B9, &H3ACAAF8F056338F0, &H7B43F50627A6778, &H4A44AB49F5ECCC77, _
        &H3BC3D6E4B679EE98, &H9CC0D4D1CF14108C, &H4406C00B206BC8A0, &H82A18854C8D72D89, _
        &H67E366B35C3C432C, &HB923DD61102B37F2, &H56AB2779D884271D, &HBE83E1B0FF1525AF, _
        &HFB7C65D4217E49A9, &H6BDBE0E76D48E7D4, &H8DF828745D9179E, &H22EA6A9ADD53BD34, _
        &HE36E141C5622200A, &H7F805D1B8CB750EE, &HAFE5C7A59F58E837, &HE27F996A4FB1C23C, _
        &HD3867DFB0775F0D0, &HD0E673DE6E88891A, &H123AEB9EAFB86C25, &H30F1D5D5C145B895, _
        &HBB434A2DEE7269E7, &H78CB67ECF931FA38, &HF33B0372323BBF9C, &H52D66336FB279C74, _
        &H505F33AC0AFB4EAA, &HE8A5CD99A2CCE187, &H534974801E2D30BB, &H8D2D5711D5876D90, _
        &H1F1A412891BC038E, &HD6E2E71D82E56648, &H74036C3A497732B7, &H89B67ED96361F5AB, _
        &HFFED95D8F1EA02A2, &HE72B3BD61464D43D, &HA6300F170BDC4820, &HEBC18760ED78A77A, _
        &HE6A6BE5A05A12138, &HB5A122A5B4F87C98, &H563C6089140B6990, &H4C46CB2E391F5DD5, _
        &HD932ADDBC9B79434, &H8EA70E42015AFF5, &HD765A6673E478CF1, &HC4FB757EAB278D99, _
        &HDF11C6862D6E0692, &HDDEB84F10D7F3B16, &H6F2EF604A665EA04, &H4A8E0F0FF0E0DFB3, _
        &HA5EDEEF83DBCBA51, &HFC4F0A2A0EA4371E, &HE83E1DA85CB38429, &HDC8FF882BA1B1CE2, _
        &HCD45505E8353E80D, &H18D19A00D4DB0717, &H34A0CFEDA5F38101, &HBE77E518887CAF2, _
        &H1E341438B3C45136, &HE05797F49089CCF9, &HFFD23F9DF2591D14, &H543DDA228595C5CD, _
        &H661F81FD99052A33, &H8736E641DB0F7B76, &H15227725418E5307, &HE25F7F46162EB2FA, _
        &H48A8B2126C13D9FE, &HAFDC541792E76EEA, &H3D912BFC6D1898F, &H31B1AAFA1B83F51B, _
        &HF1AC2796E42AB7D9, &H40A3A7D7FCD2EBAC, &H1056136D0AFBBCC5, &H7889E1DD9A6D0C85, _
        &HD33525782A7974AA, &HA7E25D09078AC09B, &HBD4138B3EAC6EDD0, &H920ABFBE71EB9E70, _
        &HA2A5D0F54FC2625C, &HC054E36B0B1290A3, &HF6DD59FF62FE932B, &H3537354511A8AC7D, _
        &HCA845E9172FADCD4, &H84F82B60329D20DC, &H79C62CE1CD672F18, &H8B09A2ADD124642C, _
        &HD0C1E96A19D9E726, &H5A786A9B4BA9500C, &HE020336634C43F3, &HC17B474AEB66D822, _
        &H6A731AE3EC9BAAC2, &H8226667AE0840258, &H67D4567691CAECA5, &H1D94155C4875ADB5, _
        &H6D00FD985B813FDF, &H51286EFCB774CD06, &H5E8834471FA744AF, &HF72CA0AEE761AE2E, _
        &HBE40E4CDAEE8E09A, &HE9970BBB5118F665, &H726E4BEB33DF1964, &H703B000729199762, _
        &H4631D816F5EF30A7, &HB880B5B51504A6BE, &H641793C37ED84B6C, &H7B21ED77F6E97D96, _
        &H776306312EF96B73, &HAE528948E86FF3F4, &H53DBD7F286A3F8F8, &H16CADCE74CFC1063, _
        &H5C19BDFA52C6DD, &H68868F5D64D46AD3, &H3A9D512CCF1E186A, &H367E62C2385660AE, _
        &HE359E7EA77DCB1D7, &H526C0773749ABE6E, &H735AE5F9D09F734B, &H493FC7CC8A558BA8, _
        &HB0B9C1533041AB45, &H321958BA470A59BD, &H852DB00B5F46C393, &H91209B2BD336B0E5, _
        &H6E604F7D659EF19F, &HB99A8AE2782CCB24, &HCCF52AB6C814C4C7, &H4727D9AFBE11727B, _
        &H7E950D0C0121B34D, &H756F435670AD471F, &HF5ADD442615A6849, &H4E87E09980B9957A, _
        &H2ACFA1DF50AEE355, &HD898263AFD2FD556, &HC8F4924DD80C8FD6, &HCF99CA3D754A173A, _
        &HFE477BACAF91BF3C, &HED5371F6D690C12D, &H831A5C285E687094, &HC5D3C90A3708A0A4, _
        &HF7F903717D06580, &H19F9BB13B8FDF27F, &HB1BD6F1B4D502843, &H1C761BA38FFF4012, _
        &HD1530C4E2E21F3B, &H8943CE69A7372C8A, &HE5184E11FEB5CE66, &H618BDB80BD736621, _
        &H7D29BAD68B574D0B, &H81BB613E25E6FE5B, &H71C9C10BC07913F, &HC7BEEB7909AC2D97, _
        &HC3E58D353BC5D757, &HEB017892F38F61E8, &HD4EFFB9C9B1CC21A, &H99727D26F494F7AB, _
        &HA3E063A2956B3E03, &H9D4A8B9A4AA09C30, &H3F6AB7D500090FB4, &H9CC0F2A057268AC0, _
        &H3DEE9D2DEDBF42D1, &H330F49C87960A972, &HC6B2720287421B41, &HAC59EC07C00369C, _
        &HEF4EAC49CB353425, &HF450244EEF0129D8, &H8ACC46E5CAF4DEB6, &H2FFEAB63989263F7, _
        &H8F7CB9FE5D7A4578, &H5BD8F7644E634635, &H427A7315BF2DC900, &H17D0C4AA2125261C, _
        &H3992486C93518E50, &HB4CBFEE0A2D7D4C3, &H7C75D6202C5DDD8D, &HDBC295D8E35B6C61, _
        &H60B369D302032B19, &HCE42685FDCE44132, &H6F3DDB9DDF65610, &H8EA4D21DB5E148F0, _
        &H20B0FCE62FCD496F, &H2C1B912358B0EE31, &HB28317B818F5A308, &HA89C1E189CA6D2CF, _
        &HC6B18576AAADBC8, &HB65DEAA91299FAE3, &HFB2B794B7F1027E7, &H4E4317F443B5BEB, _
        &H4B852D325939D0A6, &HD5AE6BEEFB207FFC, &H309682B281C7D374, &HBAE309A194C3B475, _
        &H8CC3F97B13B49F05, &H98A9422FF8293967, &H244B16B01076FF7C, &HF8BF571C663D67EE, _
        &H1F0D6758EEE30DA1, &HC9B611D97ADEB9B7, &HB7AFD5887B6C57A2, &H6290AE846B984FE1, _
        &H94DF4CDEACC1A5FD, &H58A5BD1C5483AFF, &H63166CC142BA3C37, &H8DB8526EB2F76F40, _
        &HE10880036F0D6D4E, &H9E0523C9971D311D, &H45EC2824CC7CD691, &H575B8359E62382C9, _
        &HFA9E400DC4889995, &HD1823ECB45721568, &HDAFD983B8206082F, &HAA7D29082386A8CB, _
        &H269FCD4403B87588, &H1B91F5F728BDD1E0, &HE4669F39040201F6, &H7A1D7C218CF04ADE, _
        &H65623C29D79CE5CE, &H2368449096C00BB1, &HAB9BF1879DA503BA, &HBC23ECB1A458058E, _
        &H9A58DF01BB401ECC, &HA070E868A85F143D, &H4FF188307DF2239E, &H14D565B41A641183, _
        &HEE13337452701602, &H950E3DCF3F285E09, &H59930254B9C80953, &H3BF299408930DA6D, _
        &HA955943F53691387, &HA15EDECAA9CB8784, &H29142127352BE9A0, &H76F0371FFF4E7AFB, _
        &H239F450274F2228, &HBB073AF01D5E868B, &HBFC80571C10E96C1, &HD267088568222E23, _
        &H9671A3D48E80B5B0, &H55B5D38AE193BB81, &H693AE2D0A18B04B8, &H5C48B4ECADD5335F, _
        &HFD743B194916A1CA, &H2577018134BE98C4, &HE77987E83C54A4AD, &H28E11014DA33E1B9, _
        &H270CC59E226AA213, &H71495F756D1A5F60, &H9BE853FB60AFEF77, &HADC786A7F7443DBF, _
        &H904456173B29A82, &H58BC7A66C232BD5E, &HF306558C673AC8B2, &H41F639C6B6C9772A, _
        &H216DEFE99FDA35DA, &H11640CC71C7BE615, &H93C43694565C5527, &HEA038E6246777839, _
        &HF9ABF3CE5A3E2469, &H741E768D0FD312D2, &H144B883CED652C6, &HC20B5A5BA33F8552, _
        &H1AE69633C3435A9D, &H97A28CA4088CFDEC, &H8824A43C1E96F420, &H37612FA66EEEA746, _
        &H6B4CB165F9CF0E5A, &H43AA1C06A0ABFB4A, &H7F4DC26FF162796B, &H6CBACC8E54ED9B0F, _
        &HA6B7FFEFD2BB253E, &H2E25BC95B0A29D4F, &H86D6A58BDEF1388C, &HDED74AC576B6F054, _
        &H8030BDBC2B45805D, &H3C81AF70E94D9289, &H3EFF6DDA9E3100DB, &HB38DC39FDFCC8847, _
        &H123885528D17B87E, &HF2DA0ED240B1B642, &H44CEFADCD54BF9A9, &H1312200E433C7EE6, _
        &H9FFCC84F3A78C748, &HF0CD1F72248576BB, &HEC6974053638CFE4, &H2BA7B67C0CEC4E4C, _
        &HAC2F4DF3E5CE32ED, &HCB33D14326EA4C11, &HA4E9044CC77E58BC, &H5F513293D934FCEF, _
        &H5DC9645506E55444, &H50DE418F317DE40A, &H388CB31A69DDE259, &H2DB4A83455820A86, _
        &H9010A91E84711AE9, &H4DF7F0B7B1498371, &HD62A2EABC0977179, &H22FAC097AA8D5C0E, _
        &HF49FCC2FF1DAF39B, &H487FD5C66FF29281, &HE8A30667FCDCA83F, &H2C9B4BE3D2FCCE63, _
        &HDA3FF74B93FBBBC2, &H2FA165D2FE70BA66, &HA103E279970E93D4, &HBECDEC77B0E45E71, _
        &HCFB41E723985E497, &HB70AAA025EF75017, &HD42309F03840B8E0, &H8EFC1AD035898579, _
        &H96C6920BE2B2ABC5, &H66AF4163375A9172, &H2174ABDCCA7127FB, &HB33CCEA64A72FF41, _
        &HF04A4933083066A5, &H8D970ACDD7289AF5, &H8F96E8E031C8C25E, &HF3FEC02276875D47, _
        &HEC7BF310056190DD, &HF5ADB0AEBB0F1491, &H9B50F8850FD58892, &H4975488358B74DE8, _
        &HA3354FF691531C61, &H702BBE481D2C6EE, &H89FB24057DEDED98, &HAC3075138596E902, _
        &H1D2D3580172772ED, &HEB738FC28E6BC30D, &H5854EF8F63044326, &H9E5C52325ADD3BBE, _
        &H90AA53CF325C4623, &HC1D24D51349DD067, &H2051CFEEA69EA624, &H13220F0A862E7E4F, _
        &HCE39399404E04864, &HD9C42CA47086FCB7, &H685AD2238A03E7CC, &H66484B2AB2FF1DB, _
        &HFE9D5D70EFBF79EC, &H5B13B9DD9C481854, &H15F0D475ED1509AD, &HBEBCD060EC79851, _
        &HD58C6791183AB7F8, &HD1187C5052F3EEE4, &HC95D1192E54E82FF, &H86EEA14CB9AC6CA2, _
        &H3485BEB153677D5D, &HDD191D781F8C492A, &HF60866BAA784EBF9, &H518F643BA2D08C74, _
        &H8852E956E1087C22, &HA768CB8DC410AE8D, &H38047726BFEC8E1A, &HA67738B4CD3B45AA, _
        &HAD16691CEC0DDE19, &HC6D4319380462E07, &HC5A5876D0BA61938, &H16B9FA1FA58FD840, _
        &H188AB1173CA74F18, &HABDA2F98C99C021F, &H3E0580AB134AE816, &H5F3B05B773645ABB, _
        &H2501A2BE5575F2F6, &H1B2F74004E7E8BA9, &H1CD7580371E8D953, &H7F6ED89562764E30, _
        &HB15926FF596F003D, &H9F65293DA8C5D6B9, &H6ECEF04DD690F84C, &H4782275FFF33AF88, _
        &HE41433083F820801, &HFD0DFE409A1AF9B5, &H4325A3342CDB396B, &H8AE77E62B301B252, _
        &HC36F9E9F6655615A, &H85455A2D92D32C09, &HF2C7DEA949477485, &H63CFB4C133A39EBA, _
        &H83B040CC6EBC5462, &H3B9454C8FDB326B0, &H56F56A9E87FFD78C, &H2DC2940D99F42BC6, _
        &H98F7DF096B096E2D, &H19A6E01E3AD852BF, &H42A99CCBDBD4B40B, &HA59998AF45E9C559, _
        &H366295E807D93186, &H6B48181BFAA1F773, &H1FEC57E2157A0A1D, &H4667446AF6201AD5, _
        &HE615EBCACFB0F075, &HB8F31F4F68290778, &H22713ED6CE22D11E, &H3057C1A72EC3C93B, _
        &HCB46ACC37C3F1F2F, &HDBB893FD02AAF50E, &H331FD92E600B9FCF, &HA498F96148EA3AD6, _
        &HA8D8426E8B6A83EA, &HA089B274B7735CDC, &H87F6B3731E524A11, &H118808E5CBC96749, _
        &H9906E4C7B19BD394, &HAFED7F7E9B24A20C, &H6509EADEEB3644A7, &H6C1EF1D3E8EF0EDE, _
        &HB9C97D43E9798FB4, &HA2F2D784740C28A3, &H7B8496476197566F, &H7A5BE3E6B65F069D, _
        &HF96330ED78BE6F10, &HEEE60DE77A076A15, &H2B4BEE4AA08B9BD0, &H6A56A63EC7B8894E, _
        &H2121359BA34FEF4, &H4CBF99F8283703FC, &H398071350CAF30C8, &HD0A77A89F017687A, _
        &HF1C1A9EB9E423569, &H8C7976282DEE8199, &H5D1737A5DD1F7ABD, &H4F53433C09A9FA80, _
        &HFA8B0C53DF7CA1D9, &H3FD9DCBC886CCB77, &HC040917CA91B4720, &H7DD00142F9D1DCDF, _
        &H8476FC1D4F387B58, &H23F8E7C5F3316503, &H32A2244E7E37339, &H5C87A5D750F5A74B, _
        &H82B4CC43698992E, &HDF917BECB858F63C, &H3270B8FC5BF86DDA, &H10AE72BB29B5DD76, _
        &H576AC94E7700362B, &H1AD112DAC61EFB8F, &H691BC30EC5FAA427, &HFF246311CC327143, _
        &H3142368E30E53206, &H71380E31E02CA396, &H958D5C960AAD76F1, &HF8D6F430C16DA536, _
        &HC8FFD13F1BE7E1D2, &H7578AE66004DDBE1, &H5833F01067BE646, &HBB34B5AD3BFE586D, _
        &H95F34C9A12B97F0, &H247AB64525D60CA8, &HDCDBC6F3017477D1, &H4A2E14D4DECAD24D, _
        &HBDB5E6D9BE0A1EEB, &H2A7E70F7794301AB, &HDEF42D8A270540FD, &H1078EC0A34C22C1, _
        &HE5DE511AF4C16387, &H7EBB3A52BD9A330A, &H77697857AA7D6435, &H4E831603AE4C32, _
        &HE7A21020AD78E312, &H9D41A70C6AB420F2, &H28E06C18EA1141E6, &HD2B28CBD984F6B28, _
        &H26B75F6C446E9D83, &HBA47568C4D418D7F, &HD80BADBFE6183D8E, &HE206D7F5F166044, _
        &HE258A43911CBCA3E, &H723A1746B21DC0BC, &HC7CAA854F5D7CDD3, &H7CAC32883D261D9C, _
        &H7690C26423BA942C, &H17E55524478042B8, &HE0BE477656A2389F, &H4D289B5E67AB2DA0, _
        &H44862B9C8FBBFD31, &HB47CC8049D141365, &H822C1B362B91C793, &H4EB14655FB13DFD8, _
        &H1ECBBA0714E2A97B, &H6143459D5CDE5F14, &H53A8FBF1D5F0AC89, &H97EA04D81C5E5B00, _
        &H622181A8D4FDB3F3, &HE9BCD341572A1208, &H1411258643CCE58A, &H9144C5FEA4C6E0A4, _
        &HD33D06565CF620F, &H54A48D489F219CA1, &HC43E5EAC6D63C821, &HA9728B3A72770DAF, _
        &HD7934E7B20DF87EF, &HE35503B61A3E86E5, &HCAE321FBC819D504, &H129A50B3AC60BFA6, _
        &HCD5E68EA7E9FB6C3, &HB01C90199483B1C7, &H3DE93CD5C295376C, &HAED52EDF2AB9AD13, _
        &H2E60F512C0A07884, &HBC3D86A3E36210C9, &H35269D9B163951CE, &HC7D6E2AD0CDB5FA, _
        &H59E86297D87F5733, &H298EF221898DB0E7, &H55000029D1A5AA7E, &H8BC08AE1B5061B45, _
        &HC2C31C2B6C92703A, &H94CC596BAF25EF42, &HA1D73DB22540456, &H4B6A0F9D9C4179A, _
        &HEFFDAFA2AE3D3C60, &HF7C8075BB49496C4, &H9CC5C7141D1CD4E3, &H78BD1638218E5534, _
        &HB2F11568F850246A, &HEDFABCFA9502BC29, &H796CE5F2DA23051B, &HAAE128B0DC93537C, _
        &H3A493DA0EE4B29AE, &HB5DF6B2C416895D7, &HFCABBD25122D7F37, &H70810B58105DC4B1, _
        &HE10FDD37F7882A90, &H524DCAB5518A3F5C, &H3C9E85878451255B, &H4029828119BD34E2, _
        &H74A05B6F5D3CECCB, &HB610021542E13ECA, &HFF979D12F59E2AC, &H6037DA27E4F9CC50, _
        &H5E92975A0DF1847D, &HD66DE190D3E623FE, &H5032D6B87B568048, &H9A36B7CE8235216E, _
        &H80272A7A24F64B4A, &H93EFED8B8C6916F7, &H37DDBFF44CCE1555, &H4B95DB5D4B99BD25, _
        &H92D3FDA169812FC0, &HFB1A4A9A90660BB6, &H730C196946A4B9B2, &H81E289AA7F49DA68, _
        &H64669A0F83B1A05F, &H27B3FF7D9644F48B, &HCC6B615C8DB675B3, &H674F20B9BCEBBE95, _
        &H6F31238275655982, &H5AE488713E45CF05, &HBF619F9954C21157, &HEABAC46040A8EAE9, _
        &H454C6FE9F2C0C1CD, &H419CF6496412691C, &HD3DC3BEF265B0F70, &H6D0E60F5C3578A9E, _
        &H5B0E608526323C55, &H1A46C1A9FA1B59F5, &HA9E245A17C4C8FFA, &H65CA5159DB2955D7, _
        &H5DB0A76CE35AFC2, &H81EAC77EA9113D45, &H528EF88AB6AC0A0D, &HA09EA253597BE3FF, _
        &H430DDFB3AC48CD56, &HC4B3A67AF45CE46F, &H4ECECFD8FBE2D05E, &H3EF56F10B39935F0, _
        &HB22D6829CD619C6, &H17FD460A74DF2069, &H6CF8CC8E8510ED40, &HD6C824BF3A6ECAA7, _
        &H61243D581A817049, &H48BACB6BBC163A2, &HD9A38AC27D44CC32, &H7FDDFF5BAAF410AB, _
        &HAD6D495AA804824B, &HE1A6A74F2D8C9F94, &HD4F7851235DEE8E3, &HFD4B7F886540D893, _
        &H247C20042AA4BFDA, &H96EA1C517D1327C, &HD56966B4361A6685, &H277DA5C31221057D, _
        &H94D59893A43ACFF7, &H64F0C51CCDC02281, &H3D33BCC4FF6189DB, &HE005CB184CE66AF1, _
        &HFF5CCD1D1DB99BEA, &HB0B854A7FE42980F, &H7BD46A6A718D4B9F, &HD10FA8CC22A5FD8C, _
        &HD31484952BE4BD31, &HC7FA975FCB243847, &H4886ED1E5846C407, &H28CDDB791EB70B04, _
        &HC2B00BE2F573417F, &H5C9590452180F877, &H7A6BDDFFF370EB00, &HCE509E38D6D9D6A4, _
        &HEBEB0F00647FA702, &H1DCC06CF76606F06, &HE4D9F28BA286FF0A, &HD85A305DC918C262, _
        &H475B1D8732225F54, &H2D4FB51668CCB5FE, &HA679B9D9D72BBA20, &H53841C0D912D43A5, _
        &H3B7EAA48BF12A4E8, &H781E0E47F22F1DDF, &HEFF20CE60AB50973, &H20D261D19DFFB742, _
        &H16A12B03062A2E39, &H1960EB2239650495, &H251C16FED50EB8B8, &H9AC0C330F826016E, _
        &HED152665953E7671, &H2D63194A6369570, &H5074F08394B1C987, &H70BA598C90B25CE1, _
        &H794A15810B9742F6, &HD5925E9FCAF8C6C, &H3067716CD868744E, &H910AB077E8D7731B, _
        &H6A61BBDB5AC42F61, &H93513EFBF0851567, &HF494724B9E83E9D5, &HE887E1985C09648D, _
        &H34B1D3C675370CFD, &HDC35E433BC0D255D, &HD0AAB84234131BE0, &H8042A50B48B7EAF, _
        &H9997C4EE44A3AB35, &H829A7B49201799D0, &H263B8307B7C54441, &H752F95F4FD6A6CA6, _
        &H927217402C08C6E5, &H2A8AB754A795D9EE, &HA442F7552F72943D, &H2C31334E19781208, _
        &H4FA98D7CEAEE6291, &H55C3862F665DB309, &HBD0610175D53B1F3, &H46FE6CB840413F27, _
        &H3FE03792DF0CFA59, &HCFE700372EB85E8F, &HA7BE29E7ADBCE118, &HE544EE5CDE8431DD, _
        &H8A781B1B41F1873E, &HA5C94C78A0D2F0E7, &H39412E2877B60728, &HA1265EF3AFC9A62C, _
        &HBCC2770C6A2506C5, &H3AB66DD5DCE1CE12, &HE65499D04A675B37, &H7D8F523481BFD216, _
        &HF6F64FCEC15F389, &H74EFBE618B5B13C8, &HACDC82B714273E1D, &HDD40BFE003199D17, _
        &H37E99257E7E061F8, &HFA52626904775AAA, &H8BBBF63A463D56F9, &HF0013F1543A26E64, _
        &HA8307E9F879EC898, &HCC4C27A4150177CC, &H1B432F2CCA1D3348, &HDE1D1F8F9F6FA013, _
        &H606602A047A7DDD6, &HD237AB64CC1CB2C7, &H9B938E7225FCD1D3, &HEC4E03708E0FF476, _
        &HFEB2FBDA3D03C12D, &HAE0BCED2EE43889A, &H22CB8923EBFB4F43, &H69360D013CF7396D, _
        &H855E3602D2D4E022, &H73805BAD01F784C, &H33E17A133852F546, &HDF4874058AC7B638, _
        &HBA92B29C678AA14A, &HCE89FC76CFAADCD, &H5F9D4E0908339E34, &HF1AFE9291F5923B9, _
        &H6E3480F60F4A265F, &HEEBF3A2AB29B841C, &HE21938A88F91B4AD, &H57DFEFF845C6D3C3, _
        &H2F006B0BF62CAAF2, &H62F479EF6F75EE78, &H11A55AD41C8916A9, &HF229D29084FED453, _
        &H42F1C27B16B000E6, &H2B1F76749823C074, &H4B76ECA3C2745360, &H8C98F463B91691BD, _
        &H14BCC93CF1ADE66A, &H8885213E6D458397, &H8E177DF0274D4711, &HB49B73B5503F2951, _
        &H10168168C3F96B6B, &HE3D963B63CAB0AE, &H8DFC4B5655A1DB14, &HF789F1356E14DE5C, _
        &H683E68AF4E51DAC1, &HC9A84F9D8D4B0FD9, &H3691E03F52A0F9D1, &H5ED86E46E1878E80, _
        &H3C711A0E99D07150, &H5A0865B20C4E9310, &H56FBFC1FE4F0682E, &HEA8D5DE3105EDF9B, _
        &H71ABFDB12379187A, &H2EB99DE1BEE77B9C, &H21ECC0EA33CF4523, &H59A4D7521805C7A1, _
        &H3896F5EB56AE7C72, &HAA638F3DB18F75DC, &H9F39358DABE9808E, &HB7DEFA91C00B72AC, _
        &H6B5541FD62492D92, &H6DC6DEE8F92E4D5B, &H353F57ABC4BEEA7E, &H735769D6DA5690CE, _
        &HA234AA642391484, &HF6F9508028F80D9D, &HB8E319A27AB3F215, &H31AD9C1151341A4D, _
        &H773C22A57BEF5805, &H45C7561A07968633, &HF913DA9E249DBE36, &HDA652D9B78A64C68, _
        &H4C27A97F3BC334EF, &H76621220E66B17F4, &H967743899ACD7D0B, &HF3EE5BCAE0ED6782, _
        &H409F753600C879FC, &H6D09A39B5926DB6, &H6F83AEB0317AC588, &H1E6CA4A86381F21, _
        &H66FF3462D19F3025, &H72207C24DDFD3BFB, &H4AF6B6D3E2ECE2EB, &H9C994DBEC7EA08DE, _
        &H49ACE597B09A8BC4, &HB38C4766CF0797BA, &H131B9373C57C2A75, &HB1822CCE61931E58, _
        &H9D7555B909BA1C0C, &H127FAFDD937D11D2, &H29DA3BADC66D92E4, &HA2C1D57154C2ECBC, _
        &H58C5134D82F6FE24, &H1C3AE3515B62274F, &HE907C82E01CB8126, &HF8ED091913E37FCB, _
        &H3249D8F9C80046C9, &H80CF9BEDE388FB63, &H1881539A116CF19E, &H5103F3F76BD52457, _
        &H15B7E6F5AE47F7A8, &HDBD7C6DED47E9CCF, &H44E55C410228BB1A, &HB647D4255EDB4E99, _
        &H5D11882BB8AAFC30, &HF5098BBB29D3212A, &H8FB5EA14E90296B3, &H677B942157DD025A, _
        &HFB58E7C0A390ACB5, &H89D3674C83BD4A01, &H9E2DA4DF4BF3B93B, &HFCC41E328CAB4829, _
        &H3F38C96BA582C52, &HCAD1BDBD7FD85DB2, &HBBB442C16082AE83, &HB95FE86BA5DA9AB0, _
        &HB22E04673771A93F, &H845358C9493152D8, &HBE2A488697B4541E, &H95A2DC2DD38E6966, _
        &HC02C11AC923C852B, &H2388B1990DF2A87B, &H7C8008FA1B4F37BE, &H1F70D0C84D54E503, _
        &H5490ADEC7ECE57D4, &H2B3C27D9063A3A, &H7EAEA3848030A2BF, &HC602326DED2003C0, _
        &H83A7287D69A94086, &HC57A5FCB30F57A8A, &HB56844E479EBE779, &HA373B40F05DCBCE9, _
        &HD71A786E88570EE2, &H879CBACDBDE8F6A0, &H976AD1BCC164A32F, &HAB21E25E9666D78B, _
        &H901063AAE5E5C33C, &H9818B34448698D90, &HE36487AE3E1E8ABB, &HAFBDF931893BDCB4, _
        &H6345A0DC5FBBD519, &H8628FE269B9465CA, &H1E5D01603F9C51EC, &H4DE44006A15049B7, _
        &HBF6C70E5F776CBB1, &H411218F2EF552BED, &HCB0C0708705A36A3, &HE74D14754F986044, _
        &HCD56D9430EA8280E, &HC12591D7535F5065, &HC83223F1720AEF96, &HC3A0396F7363A51F}

    End Class

End Namespace