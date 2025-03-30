; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [334 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [668 x i64] [
	i64 40218994123153105, ; 0: ExCSS.dll => 0x8ee2f649ef1ed1 => 204
	i64 120698629574877762, ; 1: Mono.Android => 0x1accec39cafe242 => 171
	i64 184471870596806994, ; 2: Svg.Skia => 0x28f60305df97952 => 226
	i64 186530032027918916, ; 3: Avalonia.ReactiveUI => 0x296b0136af76244 => 190
	i64 196720943101637631, ; 4: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 58
	i64 210515253464952879, ; 5: Xamarin.AndroidX.Collection.dll => 0x2ebe681f694702f => 237
	i64 229794953483747371, ; 6: System.ValueTuple.dll => 0x330654aed93802b => 151
	i64 232391251801502327, ; 7: Xamarin.AndroidX.SavedState.dll => 0x3399e9cbc897277 => 259
	i64 233177144301842968, ; 8: Xamarin.AndroidX.Collection.Jvm.dll => 0x33c696097d9f218 => 238
	i64 316157742385208084, ; 9: Xamarin.AndroidX.Core.Core.Ktx.dll => 0x46337caa7dc1b14 => 241
	i64 327879173351541149, ; 10: zh-Hans/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x48cdc5ef8de099d => 330
	i64 350667413455104241, ; 11: System.ServiceProcess.dll => 0x4ddd227954be8f1 => 132
	i64 404665707914610830, ; 12: Svg.Custom => 0x59da9513d08488e => 224
	i64 422779754995088667, ; 13: System.IO.UnmanagedMemoryStream => 0x5de03f27ab57d1b => 56
	i64 464346026994987652, ; 14: System.Reactive.dll => 0x671b04057e67284 => 227
	i64 499005040875495192, ; 15: NAudio.Midi => 0x6ecd270da835f18 => 215
	i64 560278790331054453, ; 16: System.Reflection.Primitives => 0x7c6829760de3975 => 95
	i64 563128987812417704, ; 17: Avalonia.Base.dll => 0x7d0a2d4b148d0a8 => 174
	i64 634256334200181332, ; 18: Microsoft.CodeAnalysis.CSharp.dll => 0x8cd54c6888b1254 => 209
	i64 634308326490598313, ; 19: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x8cd840fee8b6ba9 => 253
	i64 649145001856603771, ; 20: System.Security.SecureString => 0x90239f09b62167b => 129
	i64 684024108737575474, ; 21: Splat => 0x97e244d831b3a32 => 223
	i64 689551008517048957, ; 22: MicroCom.Runtime.dll => 0x991c6fd252bca7d => 207
	i64 707452703347108429, ; 23: Avalonia.Controls.dll => 0x9d1607c4664ea4d => 175
	i64 750875890346172408, ; 24: System.Threading.Thread => 0xa6ba5a4da7d1ff8 => 145
	i64 799765834175365804, ; 25: System.ComponentModel.dll => 0xb1956c9f18442ac => 18
	i64 872800313462103108, ; 26: Xamarin.AndroidX.DrawerLayout => 0xc1ccf42c3c21c44 => 245
	i64 918246207325748193, ; 27: NAudio => 0xcbe440f7c003be1 => 212
	i64 940822596282819491, ; 28: System.Transactions => 0xd0e792aa81923a3 => 150
	i64 960778385402502048, ; 29: System.Runtime.Handles.dll => 0xd555ed9e1ca1ba0 => 104
	i64 989127641070905171, ; 30: cs\Microsoft.CodeAnalysis.CSharp.resources => 0xdba1659538d2753 => 293
	i64 1010599046655515943, ; 31: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 95
	i64 1092282731782904681, ; 32: Avalonia.Markup.Xaml.dll => 0xf28915b7e369b69 => 178
	i64 1168571922385135248, ; 33: Antlr4.Runtime => 0x103799f8d842e690 => 173
	i64 1209178365348959744, ; 34: NAudio.WinMM.dll => 0x10c7dd5118b65600 => 217
	i64 1268860745194512059, ; 35: System.Drawing.dll => 0x119be62002c19ebb => 36
	i64 1274477032785669217, ; 36: Splat.dll => 0x11afda1bdd956c61 => 223
	i64 1301626418029409250, ; 37: System.Diagnostics.FileVersionInfo => 0x12104e54b4e833e2 => 28
	i64 1315114680217950157, ; 38: Xamarin.AndroidX.Arch.Core.Common.dll => 0x124039d5794ad7cd => 235
	i64 1354206197867287445, ; 39: Avalonia.Svg.Skia => 0x12cb1b5cb0831395 => 193
	i64 1363788917600574986, ; 40: zh-Hant\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x12ed26cb7e325e0a => 318
	i64 1404195534211153682, ; 41: System.IO.FileSystem.Watcher.dll => 0x137cb4660bd87f12 => 50
	i64 1425944114962822056, ; 42: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 115
	i64 1429127527749147140, ; 43: ru/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x13d547ea26385a04 => 315
	i64 1476839205573959279, ; 44: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 70
	i64 1492407416628141526, ; 45: it\Microsoft.CodeAnalysis.CSharp.resources => 0x14b618a368475dd6 => 297
	i64 1492954217099365037, ; 46: System.Net.HttpListener => 0x14b809f350210aad => 65
	i64 1513467482682125403, ; 47: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 170
	i64 1537168428375924959, ; 48: System.Threading.Thread.dll => 0x15551e8a954ae0df => 145
	i64 1578461236315596192, ; 49: zh-Hant\Microsoft.CodeAnalysis.resources => 0x15e7d221a250a5a0 => 292
	i64 1585507553606759907, ; 50: it\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x1600dab8396df5e3 => 310
	i64 1624659445732251991, ; 51: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0x168bf32877da9957 => 234
	i64 1628611045998245443, ; 52: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0x1699fd1e1a00b643 => 255
	i64 1651782184287836205, ; 53: System.Globalization.Calendars => 0x16ec4f2524cb982d => 40
	i64 1659332977923810219, ; 54: System.Reflection.DispatchProxy => 0x1707228d493d63ab => 89
	i64 1682513316613008342, ; 55: System.Net.dll => 0x17597cf276952bd6 => 81
	i64 1718000862390545637, ; 56: ru/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x17d790ae969a6ce5 => 302
	i64 1731380447121279447, ; 57: Newtonsoft.Json => 0x18071957e9b889d7 => 218
	i64 1735388228521408345, ; 58: System.Net.Mail.dll => 0x181556663c69b759 => 66
	i64 1743969030606105336, ; 59: System.Memory.dll => 0x1833d297e88f2af8 => 62
	i64 1767386781656293639, ; 60: System.Private.Uri.dll => 0x188704e9f5582107 => 86
	i64 1795316252682057001, ; 61: Xamarin.AndroidX.AppCompat.dll => 0x18ea3e9eac997529 => 233
	i64 1825687700144851180, ; 62: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 106
	i64 1836611346387731153, ; 63: Xamarin.AndroidX.SavedState => 0x197cf449ebe482d1 => 259
	i64 1854145951182283680, ; 64: System.Runtime.CompilerServices.VisualC => 0x19bb3feb3df2e3a0 => 102
	i64 1875417405349196092, ; 65: System.Drawing.Primitives => 0x1a06d2319b6c713c => 35
	i64 1875917498431009007, ; 66: Xamarin.AndroidX.Annotation.dll => 0x1a08990699eb70ef => 230
	i64 1930726298510463061, ; 67: CommunityToolkit.Mvvm.dll => 0x1acb5156cd389055 => 197
	i64 1936366096734835006, ; 68: Eremex.Avalonia.Icons => 0x1adf5ab4a6fd513e => 201
	i64 1956582621840560024, ; 69: de\Microsoft.CodeAnalysis.CSharp.resources => 0x1b272d8734824f98 => 294
	i64 1972384582241139858, ; 70: Microsoft.CodeAnalysis.CSharp => 0x1b5f5153d0f0bc92 => 209
	i64 1972385128188460614, ; 71: System.Security.Cryptography.Algorithms => 0x1b5f51d2edefbe46 => 119
	i64 1981742497975770890, ; 72: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x1b80904d5c241f0a => 254
	i64 2033344905091956249, ; 73: zh-Hans/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x1c37e46b05db3e19 => 317
	i64 2040001226662520565, ; 74: System.Threading.Tasks.Extensions.dll => 0x1c4f8a4ea894a6f5 => 142
	i64 2049694801020856142, ; 75: Avalonia.Themes.Fluent => 0x1c71fa8fd0d0274e => 194
	i64 2062890601515140263, ; 76: System.Threading.Tasks.Dataflow => 0x1ca0dc1289cd44a7 => 141
	i64 2064708342624596306, ; 77: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x1ca7514c5eecb152 => 270
	i64 2080945842184875448, ; 78: System.IO.MemoryMappedFiles => 0x1ce10137d8416db8 => 53
	i64 2102659300918482391, ; 79: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 35
	i64 2106033277907880740, ; 80: System.Threading.Tasks.Dataflow.dll => 0x1d3a221ba6d9cb24 => 141
	i64 2123699988094212673, ; 81: NAudio.Midi.dll => 0x1d78e5e327db5a41 => 215
	i64 2131741671470413695, ; 82: NAudio.Wasapi => 0x1d9577c178e4377f => 216
	i64 2133195048986300728, ; 83: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 218
	i64 2188974421706709258, ; 84: SkiaSharp.HarfBuzz.dll => 0x1e60cca38c3e990a => 222
	i64 2219986950236918443, ; 85: tr\Microsoft.CodeAnalysis.CSharp.resources => 0x1ecefa5e86dfd2ab => 303
	i64 2262844636196693701, ; 86: Xamarin.AndroidX.DrawerLayout.dll => 0x1f673d352266e6c5 => 245
	i64 2287834202362508563, ; 87: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 8
	i64 2287887973817120656, ; 88: System.ComponentModel.DataAnnotations.dll => 0x1fc035fd8d41f790 => 14
	i64 2304837677853103545, ; 89: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0x1ffc6da80d5ed5b9 => 258
	i64 2315304989185124968, ; 90: System.IO.FileSystem.dll => 0x20219d9ee311aa68 => 51
	i64 2323958648452149394, ; 91: cs\Microsoft.CodeAnalysis.resources => 0x20405c13f1aff092 => 280
	i64 2329709569556905518, ; 92: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x2054ca829b447e2e => 251
	i64 2335503487726329082, ; 93: System.Text.Encodings.Web => 0x2069600c4d9d1cfa => 136
	i64 2337758774805907496, ; 94: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 101
	i64 2373258765770404592, ; 95: NAudio.Core.dll => 0x20ef8245fdd3b2f0 => 214
	i64 2435477828282974838, ; 96: NAudio.Asio => 0x21cc8e2e5d258276 => 213
	i64 2455219140186822125, ; 97: Avalonia.Controls.ColorPicker.dll => 0x2212b0ccb89355ed => 186
	i64 2479423007379663237, ; 98: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x2268ae16b2cba985 => 263
	i64 2490991709351912762, ; 99: es/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x2291c7c3065d253a => 308
	i64 2497223385847772520, ; 100: System.Runtime => 0x22a7eb7046413568 => 116
	i64 2516498815742412887, ; 101: Xamarin.AndroidX.Core.SplashScreen.dll => 0x22ec665706048857 => 242
	i64 2547086958574651984, ; 102: Xamarin.AndroidX.Activity.dll => 0x2359121801df4a50 => 229
	i64 2592350477072141967, ; 103: System.Xml.dll => 0x23f9e10627330e8f => 163
	i64 2624866290265602282, ; 104: mscorlib.dll => 0x246d65fbde2db8ea => 166
	i64 2632269733008246987, ; 105: System.Net.NameResolution => 0x2487b36034f808cb => 67
	i64 2706075432581334785, ; 106: System.Net.WebSockets => 0x258de944be6c0701 => 80
	i64 2783046991838674048, ; 107: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 101
	i64 2787234703088983483, ; 108: Xamarin.AndroidX.Startup.StartupRuntime => 0x26ae3f31ef429dbb => 260
	i64 2815524396660695947, ; 109: System.Security.AccessControl => 0x2712c0857f68238b => 117
	i64 2833633450228989597, ; 110: Avalonia.Metal => 0x2753169c18903e9d => 180
	i64 2846400827948145528, ; 111: ru/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x27807278de3a1b78 => 328
	i64 2853227249135621881, ; 112: zh-Hans\Microsoft.CodeAnalysis.Scripting.resources => 0x2798b310e83196f9 => 330
	i64 2857005056680023916, ; 113: cs\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x27a61ef644e8576c => 306
	i64 2915341129155406726, ; 114: fr/Microsoft.CodeAnalysis.resources.dll => 0x28755f4f9264eb86 => 283
	i64 3017136373564924869, ; 115: System.Net.WebProxy => 0x29df058bd93f63c5 => 78
	i64 3028909397620569818, ; 116: Avalonia.dll => 0x2a08d90c9e0436da => 184
	i64 3106852385031680087, ; 117: System.Runtime.Serialization.Xml => 0x2b1dc1c88b637057 => 114
	i64 3110390492489056344, ; 118: System.Security.Cryptography.Csp.dll => 0x2b2a53ac61900058 => 121
	i64 3135773902340015556, ; 119: System.IO.FileSystem.DriveInfo.dll => 0x2b8481c008eac5c4 => 48
	i64 3218157724484662282, ; 120: System.Resources.Extensions.dll => 0x2ca931665f16740a => 228
	i64 3276991435551191081, ; 121: tr\Microsoft.CodeAnalysis.resources => 0x2d7a36593006b029 => 290
	i64 3281594302220646930, ; 122: System.Security.Principal => 0x2d8a90a198ceba12 => 128
	i64 3289520064315143713, ; 123: Xamarin.AndroidX.Lifecycle.Common => 0x2da6b911e3063621 => 250
	i64 3303437397778967116, ; 124: Xamarin.AndroidX.Annotation.Experimental => 0x2dd82acf985b2a4c => 231
	i64 3311221304742556517, ; 125: System.Numerics.Vectors.dll => 0x2df3d23ba9e2b365 => 82
	i64 3325875462027654285, ; 126: System.Runtime.Numerics => 0x2e27e21c8958b48d => 110
	i64 3328853167529574890, ; 127: System.Net.Sockets.dll => 0x2e327651a008c1ea => 75
	i64 3344514922410554693, ; 128: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x2e6a1a9a18463545 => 273
	i64 3417374630368753344, ; 129: tr/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x2f6cf41ecb8fb6c0 => 316
	i64 3437845325506641314, ; 130: System.IO.MemoryMappedFiles.dll => 0x2fb5ae1beb8f7da2 => 53
	i64 3461602852075779363, ; 131: SkiaSharp.HarfBuzz => 0x300a15741f74b523 => 222
	i64 3493805808809882663, ; 132: Xamarin.AndroidX.Tracing.Tracing.dll => 0x307c7ddf444f3427 => 261
	i64 3508450208084372758, ; 133: System.Net.Ping => 0x30b084e02d03ad16 => 69
	i64 3513997050591917592, ; 134: FluentAvalonia => 0x30c439b3164d3618 => 205
	i64 3531994851595924923, ; 135: System.Numerics => 0x31042a9aade235bb => 83
	i64 3551103847008531295, ; 136: System.Private.CoreLib.dll => 0x31480e226177735f => 172
	i64 3571415421602489686, ; 137: System.Runtime.dll => 0x319037675df7e556 => 116
	i64 3647754201059316852, ; 138: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 156
	i64 3658489898830683555, ; 139: Svg.Skia.dll => 0x32c5912df2285da3 => 226
	i64 3716579019761409177, ; 140: netstandard.dll => 0x3393f0ed5c8c5c99 => 167
	i64 3758557254239876722, ; 141: Eremex.Avalonia.Charts => 0x342913e8b6ef7672 => 199
	i64 3869649043256705283, ; 142: System.Diagnostics.Tools => 0x35b3c14d74bf0103 => 32
	i64 3919223565570527920, ; 143: System.Security.Cryptography.Encoding => 0x3663e111652bd2b0 => 122
	i64 3933965368022646939, ; 144: System.Net.Requests => 0x369840a8bfadc09b => 72
	i64 3966267475168208030, ; 145: System.Memory => 0x370b03412596249e => 62
	i64 3979027889843957915, ; 146: zh-Hant/Microsoft.CodeAnalysis.resources.dll => 0x373858c8b585709b => 292
	i64 3992675920548082773, ; 147: ru/Microsoft.CodeAnalysis.resources.dll => 0x3768d5987b863455 => 289
	i64 4006972109285359177, ; 148: System.Xml.XmlDocument => 0x379b9fe74ed9fe49 => 161
	i64 4009997192427317104, ; 149: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 113
	i64 4054446190056038390, ; 150: Microsoft.CodeAnalysis.Scripting => 0x384449541739dff6 => 211
	i64 4073500526318903918, ; 151: System.Private.Xml.dll => 0x3887fb25779ae26e => 88
	i64 4108717018440987017, ; 152: Avalonia.Diagnostics => 0x3905185bfec6f189 => 188
	i64 4148881117810174540, ; 153: System.Runtime.InteropServices.JavaScript.dll => 0x3993c9651a66aa4c => 105
	i64 4154383907710350974, ; 154: System.ComponentModel => 0x39a7562737acb67e => 18
	i64 4159398497900796080, ; 155: pt-BR\Microsoft.CodeAnalysis.Scripting.resources => 0x39b926e57aab98b0 => 327
	i64 4167269041631776580, ; 156: System.Threading.ThreadPool => 0x39d51d1d3df1cf44 => 146
	i64 4168469861834746866, ; 157: System.Security.Claims.dll => 0x39d96140fb94ebf2 => 118
	i64 4187479170553454871, ; 158: System.Linq.Expressions => 0x3a1cea1e912fa117 => 58
	i64 4201423742386704971, ; 159: Xamarin.AndroidX.Core.Core.Ktx => 0x3a4e74a233da124b => 241
	i64 4205801962323029395, ; 160: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 17
	i64 4235503420553921860, ; 161: System.IO.IsolatedStorage.dll => 0x3ac787eb9b118544 => 52
	i64 4281341464560019236, ; 162: Avalonia.ReactiveUI.dll => 0x3b6a6160e5402724 => 190
	i64 4282138915307457788, ; 163: System.Reflection.Emit => 0x3b6d36a7ddc70cfc => 92
	i64 4373617458794931033, ; 164: System.IO.Pipes.dll => 0x3cb235e806eb2359 => 55
	i64 4397634830160618470, ; 165: System.Security.SecureString.dll => 0x3d0789940f9be3e6 => 129
	i64 4477672992252076438, ; 166: System.Web.HttpUtility.dll => 0x3e23e3dcdb8ba196 => 152
	i64 4484706122338676047, ; 167: System.Globalization.Extensions.dll => 0x3e3ce07510042d4f => 41
	i64 4524629528625074585, ; 168: Avalonia.OpenGL => 0x3ecab69571f0d199 => 182
	i64 4533124835995628778, ; 169: System.Reflection.Emit.dll => 0x3ee8e505540534ea => 92
	i64 4549381600704381640, ; 170: tr\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x3f22a67651aec2c8 => 316
	i64 4633188143799146779, ; 171: fr\Microsoft.CodeAnalysis.resources => 0x404c6411b0b3191b => 283
	i64 4636684751163556186, ; 172: Xamarin.AndroidX.VersionedParcelable.dll => 0x4058d0370893015a => 264
	i64 4672453897036726049, ; 173: System.IO.FileSystem.Watcher => 0x40d7e4104a437f21 => 50
	i64 4674346441075111055, ; 174: Avalonia.Controls.DataGrid => 0x40de9d528964b48f => 187
	i64 4700350686867378774, ; 175: funnytext => 0x413b000bd27e5256 => 332
	i64 4716677666592453464, ; 176: System.Xml.XmlSerializer => 0x417501590542f358 => 162
	i64 4743821336939966868, ; 177: System.ComponentModel.Annotations => 0x41d5705f4239b194 => 13
	i64 4759461199762736555, ; 178: Xamarin.AndroidX.Lifecycle.Process.dll => 0x420d00be961cc5ab => 252
	i64 4794310189461587505, ; 179: Xamarin.AndroidX.Activity => 0x4288cfb749e4c631 => 229
	i64 4809057822547766521, ; 180: System.Drawing => 0x42bd349c3145ecf9 => 36
	i64 4814660307502931973, ; 181: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 67
	i64 4853321196694829351, ; 182: System.Runtime.Loader.dll => 0x435a75ea15de7927 => 109
	i64 5001988025904161053, ; 183: ru\Eremex.Avalonia.Controls.resources => 0x456aa19b9ec6611d => 275
	i64 5081566143765835342, ; 184: System.Resources.ResourceManager.dll => 0x4685597c05d06e4e => 99
	i64 5083120864858317402, ; 185: zh-Hans\Microsoft.CodeAnalysis.resources => 0x468adf7ebc41a25a => 291
	i64 5099468265966638712, ; 186: System.Resources.ResourceManager => 0x46c4f35ea8519678 => 99
	i64 5103417709280584325, ; 187: System.Collections.Specialized => 0x46d2fb5e161b6285 => 11
	i64 5107702058248948463, ; 188: ru\Microsoft.CodeAnalysis.CSharp.resources => 0x46e233f5d075caef => 302
	i64 5137723777516257195, ; 189: NAudio.WinMM => 0x474cdc8e08ef3fab => 217
	i64 5142942877070550468, ; 190: es/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x475f674c57ea65c4 => 321
	i64 5182934613077526976, ; 191: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 11
	i64 5207799923214932408, ; 192: fr/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x4845d270248859b8 => 322
	i64 5236762567304427675, ; 193: pt-BR\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x48acb7cf353e389b => 314
	i64 5238143335679712267, ; 194: DynamicData.dll => 0x48b19f9c65c4dc0b => 198
	i64 5244375036463807528, ; 195: System.Diagnostics.Contracts.dll => 0x48c7c34f4d59fc28 => 25
	i64 5262971552273843408, ; 196: System.Security.Principal.dll => 0x4909d4be0c44c4d0 => 128
	i64 5278787618751394462, ; 197: System.Net.WebClient.dll => 0x4942055efc68329e => 76
	i64 5290786973231294105, ; 198: System.Runtime.Loader => 0x496ca6b869b72699 => 109
	i64 5302114875655631896, ; 199: ru/Eremex.Avalonia.Charts.resources.dll => 0x4994e56339c4b418 => 274
	i64 5306356071055648198, ; 200: Svg.Model.dll => 0x49a3f6bb7b0265c6 => 225
	i64 5376510917114486089, ; 201: Xamarin.AndroidX.VectorDrawable.Animated => 0x4a9d3431719e5d49 => 263
	i64 5423376490970181369, ; 202: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 106
	i64 5440320908473006344, ; 203: Microsoft.VisualBasic.Core => 0x4b7fe70acda9f908 => 2
	i64 5446034149219586269, ; 204: System.Diagnostics.Debug => 0x4b94333452e150dd => 26
	i64 5454055681045341991, ; 205: ReactiveUI.dll => 0x4bb0b2bebde8a727 => 219
	i64 5457765010617926378, ; 206: System.Xml.Serialization => 0x4bbde05c557002ea => 157
	i64 5482521388252859714, ; 207: Eremex.Avalonia.Charts.dll => 0x4c15d427ac4b0d42 => 199
	i64 5507995362134886206, ; 208: System.Core.dll => 0x4c705499688c873e => 21
	i64 5527431512186326818, ; 209: System.IO.FileSystem.Primitives.dll => 0x4cb561acbc2a8f22 => 49
	i64 5549668961369458392, ; 210: Avalonia.Skia => 0x4d046284577006d8 => 192
	i64 5554572840649099201, ; 211: zh-Hans\Eremex.Avalonia.Charts.resources => 0x4d15ce91b5ec07c1 => 277
	i64 5570799893513421663, ; 212: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 43
	i64 5573260873512690141, ; 213: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 126
	i64 5574231584441077149, ; 214: Xamarin.AndroidX.Annotation.Jvm => 0x4d5ba617ae5f8d9d => 232
	i64 5586474322064658720, ; 215: fr\Microsoft.CodeAnalysis.CSharp.resources => 0x4d8724cc29815120 => 296
	i64 5591791169662171124, ; 216: System.Linq.Parallel => 0x4d9a087135e137f4 => 59
	i64 5610343915401270973, ; 217: Avalonia.Markup.dll => 0x4ddbf210f14456bd => 179
	i64 5650097808083101034, ; 218: System.Security.Cryptography.Algorithms.dll => 0x4e692e055d01a56a => 119
	i64 5697338526674305454, ; 219: pl\Microsoft.CodeAnalysis.CSharp.resources => 0x4f1103344791c1ae => 300
	i64 5757522595884336624, ; 220: Xamarin.AndroidX.Concurrent.Futures.dll => 0x4fe6d44bd9f885f0 => 239
	i64 5783556987928984683, ; 221: Microsoft.VisualBasic => 0x504352701bbc3c6b => 3
	i64 5979151488806146654, ; 222: System.Formats.Asn1 => 0x52fa3699a489d25e => 38
	i64 5981100626307227755, ; 223: pt-BR\Microsoft.CodeAnalysis.CSharp.resources => 0x5301235494e8a06b => 301
	i64 5984759512290286505, ; 224: System.Security.Cryptography.Primitives => 0x530e23115c33dba9 => 124
	i64 6018456290259790821, ; 225: de/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x5385da1bdd266be5 => 307
	i64 6027980928648036188, ; 226: ko\Microsoft.CodeAnalysis.Scripting.resources => 0x53a7b0b78ce53f5c => 325
	i64 6167632067760390963, ; 227: ja/Microsoft.CodeAnalysis.resources.dll => 0x5597d4b0282a8333 => 285
	i64 6222399776351216807, ; 228: System.Text.Json.dll => 0x565a67a0ffe264a7 => 137
	i64 6251069312384999852, ; 229: System.Transactions.Local => 0x56c0426b870da1ac => 149
	i64 6278736998281604212, ; 230: System.Private.DataContractSerialization => 0x57228e08a4ad6c74 => 85
	i64 6284145129771520194, ; 231: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 90
	i64 6296727896078076854, ; 232: ja/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x576278a8f506cbb6 => 298
	i64 6319713645133255417, ; 233: Xamarin.AndroidX.Lifecycle.Runtime => 0x57b42213b45b52f9 => 253
	i64 6321005050625483161, ; 234: Avalonia.Vulkan.dll => 0x57b8b89a79fb6199 => 183
	i64 6328501323422750843, ; 235: Avalonia.Dialogs => 0x57d35a6c7f33d87b => 177
	i64 6354612700029906737, ; 236: ShimSkiaSharp.dll => 0x58301e951e77ef31 => 220
	i64 6357457916754632952, ; 237: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 333
	i64 6363787360044722189, ; 238: ShimSkiaSharp => 0x5850b6e31d96280d => 220
	i64 6380710567210293407, ; 239: Avalonia.Base => 0x588cd6745526989f => 174
	i64 6384104108467684448, ; 240: pl\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x5898e4dcba540460 => 313
	i64 6384864380059521739, ; 241: Avalonia.OpenGL.dll => 0x589b9853407e12cb => 182
	i64 6397768165450447711, ; 242: es\Microsoft.CodeAnalysis.CSharp.resources => 0x58c9703fe8f9fb5f => 295
	i64 6401687960814735282, ; 243: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0x58d75d486341cfb2 => 251
	i64 6548213210057960872, ; 244: Xamarin.AndroidX.CustomView.dll => 0x5adfed387b066da8 => 244
	i64 6617685658146568858, ; 245: System.Text.Encoding.CodePages => 0x5bd6be0b4905fa9a => 133
	i64 6671798237668743565, ; 246: SkiaSharp => 0x5c96fd260152998d => 221
	i64 6713440830605852118, ; 247: System.Reflection.TypeExtensions.dll => 0x5d2aeeddb8dd7dd6 => 96
	i64 6739853162153639747, ; 248: Microsoft.VisualBasic.dll => 0x5d88c4bde075ff43 => 3
	i64 6772837112740759457, ; 249: System.Runtime.InteropServices.JavaScript => 0x5dfdf378527ec7a1 => 105
	i64 6786606130239981554, ; 250: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 33
	i64 6798329586179154312, ; 251: System.Windows => 0x5e5884bd523ca188 => 154
	i64 6814185388980153342, ; 252: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 158
	i64 6816440634817214622, ; 253: Avalonia.Dialogs.dll => 0x5e98dca46ed1789e => 177
	i64 6824903559247452477, ; 254: Avalonia.Remote.Protocol.dll => 0x5eb6eda0933e593d => 191
	i64 6833352825668324144, ; 255: Avalonia.Controls.ColorPicker => 0x5ed4f230b6df4b30 => 186
	i64 6876862101832370452, ; 256: System.Xml.Linq => 0x5f6f85a57d108914 => 155
	i64 6894844156784520562, ; 257: System.Numerics.Vectors => 0x5faf683aead1ad72 => 82
	i64 6916425539059316312, ; 258: de\Microsoft.CodeAnalysis.resources => 0x5ffc14620b11f658 => 281
	i64 7060896174307865760, ; 259: System.Threading.Tasks.Parallel.dll => 0x61fd57a90988f4a0 => 143
	i64 7083547580668757502, ; 260: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 87
	i64 7101497697220435230, ; 261: System.Configuration => 0x628d9687c0141d1e => 19
	i64 7103753931438454322, ; 262: Xamarin.AndroidX.Interpolator.dll => 0x62959a90372c7632 => 249
	i64 7112547816752919026, ; 263: System.IO.FileSystem => 0x62b4d88e3189b1f2 => 51
	i64 7219616639871433054, ; 264: ja\Microsoft.CodeAnalysis.CSharp.resources => 0x64313b153209395e => 298
	i64 7270811800166795866, ; 265: System.Linq => 0x64e71ccf51a90a5a => 61
	i64 7299370801165188114, ; 266: System.IO.Pipes.AccessControl.dll => 0x654c9311e74f3c12 => 54
	i64 7316205155833392065, ; 267: Microsoft.Win32.Primitives => 0x658861d38954abc1 => 4
	i64 7338192458477945005, ; 268: System.Reflection => 0x65d67f295d0740ad => 97
	i64 7377312882064240630, ; 269: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 17
	i64 7412872140774854801, ; 270: pt-BR/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x66dfcfefdc465091 => 301
	i64 7488575175965059935, ; 271: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 155
	i64 7489048572193775167, ; 272: System.ObjectModel => 0x67ee71ff6b419e3f => 84
	i64 7553498704560191638, ; 273: Avalonia.Skia.dll => 0x68d36b0d38aef896 => 192
	i64 7592535546311173087, ; 274: Avalonia.Fonts.Inter => 0x695e1ada366763df => 189
	i64 7592577537120840276, ; 275: System.Diagnostics.Process => 0x695e410af5b2aa54 => 29
	i64 7602111570124318452, ; 276: System.Reactive => 0x698020320025a6f4 => 227
	i64 7635123501918432388, ; 277: ru\Eremex.Avalonia.Charts.resources => 0x69f5685f5415ac84 => 274
	i64 7637303409920963731, ; 278: System.IO.Compression.ZipFile.dll => 0x69fd26fcb637f493 => 45
	i64 7654504624184590948, ; 279: System.Net.Http => 0x6a3a4366801b8264 => 64
	i64 7694700312542370399, ; 280: System.Net.Mail => 0x6ac9112a7e2cda5f => 66
	i64 7714652370974252055, ; 281: System.Private.CoreLib => 0x6b0ff375198b9c17 => 172
	i64 7735176074855944702, ; 282: Microsoft.CSharp => 0x6b58dda848e391fe => 1
	i64 7735352534559001595, ; 283: Xamarin.Kotlin.StdLib.dll => 0x6b597e2582ce8bfb => 268
	i64 7791074099216502080, ; 284: System.IO.FileSystem.AccessControl.dll => 0x6c1f749d468bcd40 => 47
	i64 7793744077883668475, ; 285: pt-BR/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x6c28f0f2208f77fb => 314
	i64 7820441508502274321, ; 286: System.Data => 0x6c87ca1e14ff8111 => 24
	i64 7836164640616011524, ; 287: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x6cbfa6390d64d704 => 234
	i64 7872210730649581996, ; 288: de/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x6d3fb5f36562f9ac => 294
	i64 7877948793867671428, ; 289: es\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x6d5418b059331784 => 308
	i64 7880754438529995359, ; 290: ko/Microsoft.CodeAnalysis.resources.dll => 0x6d5e106866a9d25f => 286
	i64 8025517457475554965, ; 291: WindowsBase => 0x6f605d9b4786ce95 => 165
	i64 8031450141206250471, ; 292: System.Runtime.Intrinsics.dll => 0x6f757159d9dc03e7 => 108
	i64 8063804418932720183, ; 293: ru/Eremex.Avalonia.Controls.resources.dll => 0x6fe8636528b5c237 => 275
	i64 8064050204834738623, ; 294: System.Collections.dll => 0x6fe942efa61731bf => 12
	i64 8083354569033831015, ; 295: Xamarin.AndroidX.Lifecycle.Common.dll => 0x702dd82730cad267 => 250
	i64 8085230611270010360, ; 296: System.Net.Http.Json.dll => 0x703482674fdd05f8 => 63
	i64 8087206902342787202, ; 297: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 27
	i64 8103644804370223335, ; 298: System.Data.DataSetExtensions.dll => 0x7075ee03be6d50e7 => 23
	i64 8113615946733131500, ; 299: System.Reflection.Extensions => 0x70995ab73cf916ec => 93
	i64 8167236081217502503, ; 300: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 168
	i64 8185542183669246576, ; 301: System.Collections => 0x7198e33f4794aa70 => 12
	i64 8187640529827139739, ; 302: Xamarin.KotlinX.Coroutines.Android => 0x71a057ae90f0109b => 272
	i64 8235972910441640993, ; 303: Avalonia.Vulkan => 0x724c0db9da9d3421 => 183
	i64 8264926008854159966, ; 304: System.Diagnostics.Process.dll => 0x72b2ea6a64a3a25e => 29
	i64 8290740647658429042, ; 305: System.Runtime.Extensions => 0x730ea0b15c929a72 => 103
	i64 8307185734628499939, ; 306: Avalonia.Android => 0x73490d698bb961e3 => 185
	i64 8318905602908530212, ; 307: System.ComponentModel.DataAnnotations => 0x7372b092055ea624 => 14
	i64 8343033485683067408, ; 308: Avalonia.MicroCom.dll => 0x73c868c07f540a10 => 181
	i64 8368701292315763008, ; 309: System.Security.Cryptography => 0x7423997c6fd56140 => 126
	i64 8373406175696939096, ; 310: ko/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x7434508d767a6858 => 325
	i64 8410500836169256799, ; 311: Eremex.Avalonia.Themes.DeltaDesign => 0x74b819f322fe8f5f => 203
	i64 8410671156615598628, ; 312: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 91
	i64 8426919725312979251, ; 313: Xamarin.AndroidX.Lifecycle.Process => 0x74f26ed7aa033133 => 252
	i64 8452111768915975231, ; 314: ko/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x754beedf64229c3f => 299
	i64 8518412311883997971, ; 315: System.Collections.Immutable => 0x76377add7c28e313 => 9
	i64 8538413690921358003, ; 316: tr/Microsoft.CodeAnalysis.resources.dll => 0x767e8a0370b312b3 => 290
	i64 8563666267364444763, ; 317: System.Private.Uri => 0x76d841191140ca5b => 86
	i64 8595707132524137149, ; 318: cs/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x774a161853648abd => 293
	i64 8598790081731763592, ; 319: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x77550a055fc61d88 => 247
	i64 8623059219396073920, ; 320: System.Net.Quic.dll => 0x77ab42ac514299c0 => 71
	i64 8626175481042262068, ; 321: Java.Interop => 0x77b654e585b55834 => 168
	i64 8626555624070097734, ; 322: tr/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x77b7aea277d8ab46 => 329
	i64 8638972117149407195, ; 323: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 1
	i64 8648495978913578441, ; 324: Microsoft.Win32.Registry.dll => 0x7805a1456889bdc9 => 5
	i64 8684531736582871431, ; 325: System.IO.Compression.FileSystem => 0x7885a79a0fa0d987 => 44
	i64 8725526185868997716, ; 326: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 27
	i64 8747977504141423047, ; 327: zh-Hans\Microsoft.CodeAnalysis.CSharp.resources => 0x79670f30f57531c7 => 304
	i64 8834232125107082525, ; 328: ExCSS => 0x7a997f4fe05a151d => 204
	i64 8853378295825400934, ; 329: Xamarin.Kotlin.StdLib.Common.dll => 0x7add84a720d38466 => 269
	i64 8928170594780924904, ; 330: cs/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x7be73bda3c014be8 => 306
	i64 8941376889969657626, ; 331: System.Xml.XDocument => 0x7c1626e87187471a => 158
	i64 8951477988056063522, ; 332: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0x7c3a09cd9ccf5e22 => 257
	i64 8954753533646919997, ; 333: System.Runtime.Serialization.Json => 0x7c45ace50032d93d => 112
	i64 8962916719318709492, ; 334: Avalonia.Themes.Fluent.dll => 0x7c62ad44c666b4f4 => 194
	i64 8987845817414925545, ; 335: Avalonia.Remote.Protocol => 0x7cbb3e26baff3ce9 => 191
	i64 9138683372487561558, ; 336: System.Security.Cryptography.Csp => 0x7ed3201bc3e3d156 => 121
	i64 9146833000203878303, ; 337: pl/Microsoft.CodeAnalysis.resources.dll => 0x7ef01426d4f8ff9f => 287
	i64 9165872221346508209, ; 338: es/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x7f33b838f1faadb1 => 295
	i64 9248940107580716988, ; 339: Svg.Custom.dll => 0x805ad6065d3637bc => 224
	i64 9303633714348615373, ; 340: zh-Hans\Eremex.Avalonia.Controls3D.resources => 0x811d25920818e6cd => 279
	i64 9324707631942237306, ; 341: Xamarin.AndroidX.AppCompat => 0x8168042fd44a7c7a => 233
	i64 9447068171083017806, ; 342: de\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x831aba72ea4d6e4e => 307
	i64 9468215723722196442, ; 343: System.Xml.XPath.XDocument.dll => 0x8365dc09353ac5da => 159
	i64 9554839972845591462, ; 344: System.ServiceModel.Web => 0x84999c54e32a1ba6 => 131
	i64 9578780224854453762, ; 345: NAudio.Wasapi.dll => 0x84eea9dd2bfc6e02 => 216
	i64 9584643793929893533, ; 346: System.IO.dll => 0x85037ebfbbd7f69d => 57
	i64 9640812368965969847, ; 347: it/Microsoft.CodeAnalysis.resources.dll => 0x85cb0bc53668a7b7 => 284
	i64 9659729154652888475, ; 348: System.Text.RegularExpressions => 0x860e407c9991dd9b => 138
	i64 9662334977499516867, ; 349: System.Numerics.dll => 0x8617827802b0cfc3 => 83
	i64 9667360217193089419, ; 350: System.Diagnostics.StackTrace => 0x86295ce5cd89898b => 30
	i64 9702891218465930390, ; 351: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 10
	i64 9749644614323256758, ; 352: zh-Hans\Eremex.Avalonia.Controls.resources => 0x874db21ef309bdb6 => 278
	i64 9776157902332307015, ; 353: de/Microsoft.CodeAnalysis.resources.dll => 0x87abe3d0dca52647 => 281
	i64 9776521250654317172, ; 354: tr\Microsoft.CodeAnalysis.Scripting.resources => 0x87ad2e477c4eea74 => 329
	i64 9780299811128225677, ; 355: Microsoft.CodeAnalysis.CSharp.Scripting.dll => 0x87ba9adc271e2f8d => 210
	i64 9808709177481450983, ; 356: Mono.Android.dll => 0x881f890734e555e7 => 171
	i64 9815966120248698980, ; 357: it\Microsoft.CodeAnalysis.resources => 0x8839512ddcb16864 => 284
	i64 9825649861376906464, ; 358: Xamarin.AndroidX.Concurrent.Futures => 0x885bb87d8abc94e0 => 239
	i64 9834056768316610435, ; 359: System.Transactions.dll => 0x8879968718899783 => 150
	i64 9836529246295212050, ; 360: System.Reflection.Metadata => 0x88825f3bbc2ac012 => 94
	i64 9865124420436172353, ; 361: cs\Microsoft.CodeAnalysis.Scripting.resources => 0x88e7f66489211e41 => 319
	i64 9868012133014750939, ; 362: ja\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x88f238c08396c6db => 311
	i64 9907349773706910547, ; 363: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x897dfa20b758db53 => 247
	i64 9933555792566666578, ; 364: System.Linq.Queryable.dll => 0x89db145cf475c552 => 60
	i64 9934914332219610418, ; 365: cs/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x89dfe7f29de31532 => 319
	i64 9974604633896246661, ; 366: System.Xml.Serialization.dll => 0x8a6cea111a59dd85 => 157
	i64 10004628763249497471, ; 367: Avalonia.Themes.Simple => 0x8ad794da7724557f => 195
	i64 10038780035334861115, ; 368: System.Net.Http.dll => 0x8b50e941206af13b => 64
	i64 10051358222726253779, ; 369: System.Private.Xml => 0x8b7d990c97ccccd3 => 88
	i64 10078727084704864206, ; 370: System.Net.WebSockets.Client => 0x8bded4e257f117ce => 79
	i64 10089571585547156312, ; 371: System.IO.FileSystem.AccessControl => 0x8c055be67469bb58 => 47
	i64 10105485790837105934, ; 372: System.Threading.Tasks.Parallel => 0x8c3de5c91d9a650e => 143
	i64 10216170220350431252, ; 373: Antlr4.Runtime.dll => 0x8dc720b014e96014 => 173
	i64 10226222362177979215, ; 374: Xamarin.Kotlin.StdLib.Jdk7 => 0x8dead70ebbc6434f => 270
	i64 10229024438826829339, ; 375: Xamarin.AndroidX.CustomView => 0x8df4cb880b10061b => 244
	i64 10236703004850800690, ; 376: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 74
	i64 10245369515835430794, ; 377: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 91
	i64 10253386210042008591, ; 378: Avalonia.Markup.Xaml => 0x8e4b586eea71540f => 178
	i64 10266173615553453214, ; 379: Avalonia.Xaml.Interactivity.dll => 0x8e78c682c58d1c9e => 196
	i64 10289148163036224226, ; 380: NAudio.Asio.dll => 0x8eca65bd73231ae2 => 213
	i64 10309569382039181079, ; 381: it/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0x8f12f2bb03b05717 => 310
	i64 10321854143672141184, ; 382: Xamarin.Jetbrains.Annotations.dll => 0x8f3e97a7f8f8c580 => 267
	i64 10347389959537838075, ; 383: ru\Microsoft.CodeAnalysis.resources => 0x8f9950586ab247fb => 289
	i64 10360651442923773544, ; 384: System.Text.Encoding => 0x8fc86d98211c1e68 => 135
	i64 10364469296367737616, ; 385: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 90
	i64 10376576884623852283, ; 386: Xamarin.AndroidX.Tracing.Tracing => 0x900101b2f888c2fb => 261
	i64 10387253178616470675, ; 387: ru\Eremex.Avalonia.Controls3D.resources => 0x9026efbb5f7b5c93 => 276
	i64 10406448008575299332, ; 388: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x906b2153fcb3af04 => 273
	i64 10426284384445314437, ; 389: es\Microsoft.CodeAnalysis.resources => 0x90b19a682610b585 => 282
	i64 10430153318873392755, ; 390: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 240
	i64 10466034231130677304, ; 391: MicroCom.Runtime => 0x913ed2ae899f6038 => 207
	i64 10503238815856555353, ; 392: ko\Microsoft.CodeAnalysis.resources => 0x91c3000df2397559 => 286
	i64 10505200486220552556, ; 393: ja/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x91c9f82eeb6fd96c => 324
	i64 10546663366131771576, ; 394: System.Runtime.Serialization.Json.dll => 0x925d4673efe8e8b8 => 112
	i64 10566960649245365243, ; 395: System.Globalization.dll => 0x92a562b96dcd13fb => 42
	i64 10595762989148858956, ; 396: System.Xml.XPath.XDocument => 0x930bb64cc472ea4c => 159
	i64 10670374202010151210, ; 397: Microsoft.Win32.Primitives.dll => 0x9414c8cd7b4ea92a => 4
	i64 10714184849103829812, ; 398: System.Runtime.Extensions.dll => 0x94b06e5aa4b4bb34 => 103
	i64 10785150219063592792, ; 399: System.Net.Primitives => 0x95ac8cfb68830758 => 70
	i64 10822644899632537592, ; 400: System.Linq.Queryable => 0x9631c23204ca5ff8 => 60
	i64 10830817578243619689, ; 401: System.Formats.Tar => 0x964ecb340a447b69 => 39
	i64 10847732767863316357, ; 402: Xamarin.AndroidX.Arch.Core.Common => 0x968ae37a86db9f85 => 235
	i64 10881928253345575956, ; 403: de\Microsoft.CodeAnalysis.Scripting.resources => 0x970460176bf33414 => 320
	i64 10899834349646441345, ; 404: System.Web => 0x9743fd975946eb81 => 153
	i64 10943875058216066601, ; 405: System.IO.UnmanagedMemoryStream.dll => 0x97e07461df39de29 => 56
	i64 10964653383833615866, ; 406: System.Diagnostics.Tracing => 0x982a4628ccaffdfa => 34
	i64 11011906640654766267, ; 407: pl/Microsoft.CodeAnalysis.CSharp.resources.dll => 0x98d226befffe10bb => 300
	i64 11019817191295005410, ; 408: Xamarin.AndroidX.Annotation.Jvm.dll => 0x98ee415998e1b2e2 => 232
	i64 11023048688141570732, ; 409: System.Core => 0x98f9bc61168392ac => 21
	i64 11037814507248023548, ; 410: System.Xml => 0x992e31d0412bf7fc => 163
	i64 11067960147806077617, ; 411: zh-Hant\Microsoft.CodeAnalysis.Scripting.resources => 0x99994b1d262942b1 => 331
	i64 11162124722117608902, ; 412: Xamarin.AndroidX.ViewPager => 0x9ae7d54b986d05c6 => 265
	i64 11188319605227840848, ; 413: System.Threading.Overlapped => 0x9b44e5671724e550 => 140
	i64 11216600183782743182, ; 414: Svg.Model => 0x9ba95e7065f39c8e => 225
	i64 11235648312900863002, ; 415: System.Reflection.DispatchProxy.dll => 0x9bed0a9c8fac441a => 89
	i64 11299661109949763898, ; 416: Xamarin.AndroidX.Collection.Jvm => 0x9cd075e94cda113a => 238
	i64 11329751333533450475, ; 417: System.Threading.Timer.dll => 0x9d3b5ccf6cc500eb => 147
	i64 11340910727871153756, ; 418: Xamarin.AndroidX.CursorAdapter => 0x9d630238642d465c => 243
	i64 11347436699239206956, ; 419: System.Xml.XmlSerializer.dll => 0x9d7a318e8162502c => 162
	i64 11431825214787067294, ; 420: ko\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0x9ea60076e5bc9d9e => 312
	i64 11432101114902388181, ; 421: System.AppContext => 0x9ea6fb64e61a9dd5 => 6
	i64 11446671985764974897, ; 422: Mono.Android.Export => 0x9edabf8623efc131 => 169
	i64 11448276831755070604, ; 423: System.Diagnostics.TextWriterTraceListener => 0x9ee0731f77186c8c => 31
	i64 11485890710487134646, ; 424: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 107
	i64 11509430195292955891, ; 425: it/Microsoft.CodeAnalysis.Scripting.resources.dll => 0x9fb9b5c8759764f3 => 323
	i64 11529969570048099689, ; 426: Xamarin.AndroidX.ViewPager.dll => 0xa002ae3c4dc7c569 => 265
	i64 11564861549255168062, ; 427: Microsoft.CodeAnalysis.dll => 0xa07ea44e47ed903e => 208
	i64 11580057168383206117, ; 428: Xamarin.AndroidX.Annotation => 0xa0b4a0a4103262e5 => 230
	i64 11591352189662810718, ; 429: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0xa0dcc167234c525e => 260
	i64 11597940890313164233, ; 430: netstandard => 0xa0f429ca8d1805c9 => 167
	i64 11672361001936329215, ; 431: Xamarin.AndroidX.Interpolator => 0xa1fc8e7d0a8999ff => 249
	i64 11674412572205198830, ; 432: it\Microsoft.CodeAnalysis.Scripting.resources => 0xa203d86177a52dee => 323
	i64 11691353810037938030, ; 433: pl\Microsoft.CodeAnalysis.resources => 0xa2400858c6b8976e => 287
	i64 11692977985522001935, ; 434: System.Threading.Overlapped.dll => 0xa245cd869980680f => 140
	i64 11707554492040141440, ; 435: System.Linq.Parallel.dll => 0xa27996c7fe94da80 => 59
	i64 11743665907891708234, ; 436: System.Threading.Tasks => 0xa2f9e1ec30c0214a => 144
	i64 11757122496836848454, ; 437: Microsoft.CodeAnalysis.CSharp.Scripting => 0xa329b09e74b6cb46 => 210
	i64 11854590080824757919, ; 438: zh-Hans/Eremex.Avalonia.Controls.resources.dll => 0xa483f6dec56dce9f => 278
	i64 11917635639362386622, ; 439: ReactiveUI => 0xa563f278bebafabe => 219
	i64 11991047634523762324, ; 440: System.Net => 0xa668c24ad493ae94 => 81
	i64 11997227339607644824, ; 441: Avalonia.Controls.DataGrid.dll => 0xa67eb6b38aeb1a98 => 187
	i64 12040886584167504988, ; 442: System.Net.ServicePoint => 0xa719d28d8e121c5c => 74
	i64 12063623837170009990, ; 443: System.Security => 0xa76a99f6ce740786 => 130
	i64 12070042708472425620, ; 444: de/Microsoft.CodeAnalysis.Scripting.resources.dll => 0xa78167e4be4aec94 => 320
	i64 12096697103934194533, ; 445: System.Diagnostics.Contracts => 0xa7e019eccb7e8365 => 25
	i64 12102847907131387746, ; 446: System.Buffers => 0xa7f5f40c43256f62 => 7
	i64 12107153474885735932, ; 447: Avalonia.DesignerSupport.dll => 0xa8053ff05fb355fc => 176
	i64 12123043025855404482, ; 448: System.Reflection.Extensions.dll => 0xa83db366c0e359c2 => 93
	i64 12137774235383566651, ; 449: Xamarin.AndroidX.VectorDrawable => 0xa872095bbfed113b => 262
	i64 12145679461940342714, ; 450: System.Text.Json => 0xa88e1f1ebcb62fba => 137
	i64 12201331334810686224, ; 451: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 113
	i64 12269460666702402136, ; 452: System.Collections.Immutable.dll => 0xaa45e178506c9258 => 9
	i64 12319827376581239000, ; 453: Avalonia => 0xaaf8d1b9cb41e0d8 => 184
	i64 12332222936682028543, ; 454: System.Runtime.Handles => 0xab24db6c07db5dff => 104
	i64 12375446203996702057, ; 455: System.Configuration.dll => 0xabbe6ac12e2e0569 => 19
	i64 12451044538927396471, ; 456: Xamarin.AndroidX.Fragment.dll => 0xaccaff0a2955b677 => 248
	i64 12466513435562512481, ; 457: Xamarin.AndroidX.Loader.dll => 0xad01f3eb52569061 => 256
	i64 12475113361194491050, ; 458: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 333
	i64 12517810545449516888, ; 459: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 33
	i64 12519721031386090644, ; 460: Eremex.Avalonia.Icons.dll => 0xadbefbf21ac14c94 => 201
	i64 12550732019250633519, ; 461: System.IO.Compression => 0xae2d28465e8e1b2f => 46
	i64 12699999919562409296, ; 462: System.Diagnostics.StackTrace.dll => 0xb03f76a3ad01c550 => 30
	i64 12700543734426720211, ; 463: Xamarin.AndroidX.Collection => 0xb041653c70d157d3 => 237
	i64 12708238894395270091, ; 464: System.IO => 0xb05cbbf17d3ba3cb => 57
	i64 12708922737231849740, ; 465: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 134
	i64 12717050818822477433, ; 466: System.Runtime.Serialization.Xml.dll => 0xb07c0a5786811679 => 114
	i64 12822330414412999099, ; 467: zh-Hant\Microsoft.CodeAnalysis.CSharp.resources => 0xb1f2119387c629bb => 305
	i64 12828192437253469131, ; 468: Xamarin.Kotlin.StdLib.Jdk8.dll => 0xb206e50e14d873cb => 271
	i64 12831179148886114003, ; 469: it/Microsoft.CodeAnalysis.CSharp.resources.dll => 0xb211817412c17ed3 => 297
	i64 12835242264250840079, ; 470: System.IO.Pipes => 0xb21ff0d5d6c0740f => 55
	i64 12835543923467107475, ; 471: pt-BR\Microsoft.CodeAnalysis.resources => 0xb2210331592e3c93 => 288
	i64 12843770487262409629, ; 472: System.AppContext.dll => 0xb23e3d357debf39d => 6
	i64 12859557719246324186, ; 473: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 77
	i64 12991459499837607210, ; 474: Microsoft.CodeAnalysis => 0xb44aef9559b1cd2a => 208
	i64 12998524970302822152, ; 475: Avalonia.Fonts.Inter.dll => 0xb464099762f1d708 => 189
	i64 13039467033719597668, ; 476: tr/Microsoft.CodeAnalysis.CSharp.resources.dll => 0xb4f57e2e5d45ea64 => 303
	i64 13056307388510335261, ; 477: zh-Hant/Microsoft.CodeAnalysis.Scripting.resources.dll => 0xb53152650277711d => 331
	i64 13068258254871114833, ; 478: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 111
	i64 13106026140046202731, ; 479: HarfBuzzSharp.dll => 0xb5e1f555ee70176b => 206
	i64 13129914918964716986, ; 480: Xamarin.AndroidX.Emoji2.dll => 0xb636d40db3fe65ba => 246
	i64 13166897321255124987, ; 481: ko\Microsoft.CodeAnalysis.CSharp.resources => 0xb6ba375a3b743ffb => 299
	i64 13173818576982874404, ; 482: System.Runtime.CompilerServices.VisualC.dll => 0xb6d2ce32a8819924 => 102
	i64 13233222243778831259, ; 483: es\Microsoft.CodeAnalysis.Scripting.resources => 0xb7a5d984a23c239b => 321
	i64 13270034446771288861, ; 484: zh-Hant/Microsoft.CodeAnalysis.CSharp.resources.dll => 0xb828a2058cfffb1d => 305
	i64 13329215392816850629, ; 485: fr/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0xb8fae2c74f8466c5 => 309
	i64 13343850469010654401, ; 486: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 170
	i64 13370592475155966277, ; 487: System.Runtime.Serialization => 0xb98de304062ea945 => 115
	i64 13401370062847626945, ; 488: Xamarin.AndroidX.VectorDrawable.dll => 0xb9fb3b1193964ec1 => 262
	i64 13431476299110033919, ; 489: System.Net.WebClient => 0xba663087f18829ff => 76
	i64 13454009404024712428, ; 490: Xamarin.Google.Guava.ListenableFuture => 0xbab63e4543a86cec => 266
	i64 13463706743370286408, ; 491: System.Private.DataContractSerialization.dll => 0xbad8b1f3069e0548 => 85
	i64 13465488254036897740, ; 492: Xamarin.Kotlin.StdLib => 0xbadf06394d106fcc => 268
	i64 13491513212026656886, ; 493: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0xbb3b7bc905569876 => 236
	i64 13553170703867477024, ; 494: Microsoft.CodeAnalysis.Scripting.dll => 0xbc1688f288407c20 => 211
	i64 13578472628727169633, ; 495: System.Xml.XPath => 0xbc706ce9fba5c261 => 160
	i64 13580399111273692417, ; 496: Microsoft.VisualBasic.Core.dll => 0xbc77450a277fbd01 => 2
	i64 13585332712099178800, ; 497: pl/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0xbc88cc1f9c2a6930 => 313
	i64 13601819457160100262, ; 498: funnytext.Android.dll => 0xbcc35ebb130639a6 => 0
	i64 13647894001087880694, ; 499: System.Data.dll => 0xbd670f48cb071df6 => 24
	i64 13648427842443111366, ; 500: FluentAvalonia.dll => 0xbd68f4cf6b5f6bc6 => 205
	i64 13702626353344114072, ; 501: System.Diagnostics.Tools.dll => 0xbe29821198fb6d98 => 32
	i64 13710614125866346983, ; 502: System.Security.AccessControl.dll => 0xbe45e2e7d0b769e7 => 117
	i64 13713329104121190199, ; 503: System.Dynamic.Runtime => 0xbe4f8829f32b5737 => 37
	i64 13717397318615465333, ; 504: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 16
	i64 13768883594457632599, ; 505: System.IO.IsolatedStorage => 0xbf14e6adb159cf57 => 52
	i64 13828521679616088467, ; 506: Xamarin.Kotlin.StdLib.Common => 0xbfe8c733724e1993 => 269
	i64 13838448951611437224, ; 507: Avalonia.Markup => 0xc00c0c00932d30a8 => 179
	i64 13852283301847103176, ; 508: Eremex.GuardantAPI => 0xc03d32453eabf6c8 => 202
	i64 13877738829891856469, ; 509: Eremex.GuardantAPI.dll => 0xc097a1f039596855 => 202
	i64 13881769479078963060, ; 510: System.Console.dll => 0xc0a5f3cade5c6774 => 20
	i64 13911222732217019342, ; 511: System.Security.Cryptography.OpenSsl.dll => 0xc10e975ec1226bce => 123
	i64 13928444506500929300, ; 512: System.Windows.dll => 0xc14bc67b8bba9714 => 154
	i64 13957763705375634936, ; 513: ru\Microsoft.CodeAnalysis.Scripting.resources => 0xc1b3f0237dc135f8 => 328
	i64 13959074834287824816, ; 514: Xamarin.AndroidX.Fragment => 0xc1b8989a7ad20fb0 => 248
	i64 14004818818307233107, ; 515: pl/Microsoft.CodeAnalysis.Scripting.resources.dll => 0xc25b1c83b8e89553 => 326
	i64 14075334701871371868, ; 516: System.ServiceModel.Web.dll => 0xc355a25647c5965c => 131
	i64 14125464355221830302, ; 517: System.Threading.dll => 0xc407bafdbc707a9e => 148
	i64 14145279715929945620, ; 518: pt-BR/Microsoft.CodeAnalysis.Scripting.resources.dll => 0xc44e20f4ec8af614 => 327
	i64 14212104595480609394, ; 519: System.Security.Cryptography.Cng.dll => 0xc53b89d4a4518272 => 120
	i64 14220608275227875801, ; 520: System.Diagnostics.FileVersionInfo.dll => 0xc559bfe1def019d9 => 28
	i64 14223901617201535728, ; 521: Eremex.Avalonia.Controls.dll => 0xc5657328cd66daf0 => 200
	i64 14226382999226559092, ; 522: System.ServiceProcess => 0xc56e43f6938e2a74 => 132
	i64 14232023429000439693, ; 523: System.Resources.Writer.dll => 0xc5824de7789ba78d => 100
	i64 14236779789349124699, ; 524: cs/Microsoft.CodeAnalysis.resources.dll => 0xc59333c9e99d7a5b => 280
	i64 14254574811015963973, ; 525: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 134
	i64 14270428367777445898, ; 526: Eremex.Avalonia.Themes.DeltaDesign.dll => 0xc60abefe1608380a => 203
	i64 14288893986376248493, ; 527: ja/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0xc64c595ff32becad => 311
	i64 14298246716367104064, ; 528: System.Web.dll => 0xc66d93a217f4e840 => 153
	i64 14327695147300244862, ; 529: System.Reflection.dll => 0xc6d632d338eb4d7e => 97
	i64 14327709162229390963, ; 530: System.Security.Cryptography.X509Certificates => 0xc6d63f9253cade73 => 125
	i64 14346402571976470310, ; 531: System.Net.Ping.dll => 0xc718a920f3686f26 => 69
	i64 14461014870687870182, ; 532: System.Net.Requests.dll => 0xc8afd8683afdece6 => 72
	i64 14495724990987328804, ; 533: Xamarin.AndroidX.ResourceInspection.Annotation => 0xc92b2913e18d5d24 => 258
	i64 14551742072151931844, ; 534: System.Text.Encodings.Web.dll => 0xc9f22c50f1b8fbc4 => 136
	i64 14561513370130550166, ; 535: System.Security.Cryptography.Primitives.dll => 0xca14e3428abb8d96 => 124
	i64 14574160591280636898, ; 536: System.Net.Quic => 0xca41d1d72ec783e2 => 71
	i64 14622043554576106986, ; 537: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 111
	i64 14690985099581930927, ; 538: System.Web.HttpUtility => 0xcbe0dd1ca5233daf => 152
	i64 14792063746108907174, ; 539: Xamarin.Google.Guava.ListenableFuture.dll => 0xcd47f79af9c15ea6 => 266
	i64 14832630590065248058, ; 540: System.Security.Claims => 0xcdd816ef5d6e873a => 118
	i64 14852515768018889994, ; 541: Xamarin.AndroidX.CursorAdapter.dll => 0xce1ebc6625a76d0a => 243
	i64 14863661193551929348, ; 542: funnytext.dll => 0xce46551abf6be004 => 332
	i64 14912225920358050525, ; 543: System.Security.Principal.Windows => 0xcef2de7759506add => 127
	i64 14931407803744742450, ; 544: HarfBuzzSharp => 0xcf3704499ab36c32 => 206
	i64 14935719434541007538, ; 545: System.Text.Encoding.CodePages.dll => 0xcf4655b160b702b2 => 133
	i64 14958833204550082229, ; 546: Eremex.Avalonia.Controls => 0xcf98738b53869eb5 => 200
	i64 14973015887391301737, ; 547: fr\Microsoft.CodeAnalysis.Scripting.resources => 0xcfcad69e803d2069 => 322
	i64 14984936317414011727, ; 548: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 77
	i64 14987728460634540364, ; 549: System.IO.Compression.dll => 0xcfff1ba06622494c => 46
	i64 15015154896917945444, ; 550: System.Net.Security.dll => 0xd0608bd33642dc64 => 73
	i64 15024878362326791334, ; 551: System.Net.Http.Json => 0xd0831743ebf0f4a6 => 63
	i64 15065174275109340882, ; 552: zh-Hans\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0xd112402f3b3ec6d2 => 317
	i64 15071021337266399595, ; 553: System.Resources.Reader.dll => 0xd127060e7a18a96b => 98
	i64 15076659072870671916, ; 554: System.ObjectModel.dll => 0xd13b0d8c1620662c => 84
	i64 15115185479366240210, ; 555: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 43
	i64 15133485256822086103, ; 556: System.Linq.dll => 0xd204f0a9127dd9d7 => 61
	i64 15150743910298169673, ; 557: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xd2424150783c3149 => 257
	i64 15195733091524337868, ; 558: ja\Microsoft.CodeAnalysis.resources => 0xd2e216bc7df4e0cc => 285
	i64 15234786388537674379, ; 559: System.Dynamic.Runtime.dll => 0xd36cd580c5be8a8b => 37
	i64 15250465174479574862, ; 560: System.Globalization.Calendars.dll => 0xd3a489469852174e => 40
	i64 15279429628684179188, ; 561: Xamarin.KotlinX.Coroutines.Android.dll => 0xd40b704b1c4c96f4 => 272
	i64 15299439993936780255, ; 562: System.Xml.XPath.dll => 0xd452879d55019bdf => 160
	i64 15338463749992804988, ; 563: System.Resources.Reader => 0xd4dd2b839286f27c => 98
	i64 15344154949114261798, ; 564: fr/Microsoft.CodeAnalysis.CSharp.resources.dll => 0xd4f163a12081f126 => 296
	i64 15370334346939861994, ; 565: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 240
	i64 15376059877735500810, ; 566: es/Microsoft.CodeAnalysis.resources.dll => 0xd562bcfe318f8c0a => 282
	i64 15389372189903242610, ; 567: zh-Hans/Microsoft.CodeAnalysis.resources.dll => 0xd592087867754572 => 291
	i64 15463022123026093319, ; 568: ru\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0xd697b0b1bcfa1507 => 315
	i64 15478421470751140539, ; 569: ko/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0xd6ce6651c7e6aabb => 312
	i64 15526743539506359484, ; 570: System.Text.Encoding.dll => 0xd77a12fc26de2cbc => 135
	i64 15527772828719725935, ; 571: System.Console => 0xd77dbb1e38cd3d6f => 20
	i64 15530465045505749832, ; 572: System.Net.HttpListener.dll => 0xd7874bacc9fdb348 => 65
	i64 15541854775306130054, ; 573: System.Security.Cryptography.X509Certificates.dll => 0xd7afc292e8d49286 => 125
	i64 15557562860424774966, ; 574: System.Net.Sockets => 0xd7e790fe7a6dc536 => 75
	i64 15576494632235117767, ; 575: funnytext.Android => 0xd82ad35718272cc7 => 0
	i64 15582737692548360875, ; 576: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xd841015ed86f6aab => 255
	i64 15609085926864131306, ; 577: System.dll => 0xd89e9cf3334914ea => 164
	i64 15619452137160576618, ; 578: Avalonia.Svg.Skia.dll => 0xd8c370f69dac0e6a => 193
	i64 15661133872274321916, ; 579: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 156
	i64 15710114879900314733, ; 580: Microsoft.Win32.Registry => 0xda058a3f5d096c6d => 5
	i64 15755368083429170162, ; 581: System.IO.FileSystem.Primitives => 0xdaa64fcbde529bf2 => 49
	i64 15817206913877585035, ; 582: System.Threading.Tasks.dll => 0xdb8201e29086ac8b => 144
	i64 15824196878959156486, ; 583: Avalonia.Xaml.Interactivity => 0xdb9ad738a2349d06 => 196
	i64 15825747108975065274, ; 584: DynamicData => 0xdba05925af9fb8ba => 198
	i64 15847085070278954535, ; 585: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 139
	i64 15885744048853936810, ; 586: System.Resources.Writer => 0xdc75800bd0b6eaaa => 100
	i64 15934062614519587357, ; 587: System.Security.Cryptography.OpenSsl => 0xdd2129868f45a21d => 123
	i64 15937190497610202713, ; 588: System.Security.Cryptography.Cng => 0xdd2c465197c97e59 => 120
	i64 15963349826457351533, ; 589: System.Threading.Tasks.Extensions => 0xdd893616f748b56d => 142
	i64 15971679995444160383, ; 590: System.Formats.Tar.dll => 0xdda6ce5592a9677f => 39
	i64 16002543873629058066, ; 591: zh-Hant/Microsoft.CodeAnalysis.CSharp.Scripting.resources.dll => 0xde1474de32437412 => 318
	i64 16018552496348375205, ; 592: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 68
	i64 16053592439138609341, ; 593: pt-BR/Microsoft.CodeAnalysis.resources.dll => 0xdec9d1448fc0e8bd => 288
	i64 16054465462676478687, ; 594: System.Globalization.Extensions => 0xdecceb47319bdadf => 41
	i64 16063929133569271981, ; 595: Avalonia.Android.dll => 0xdeee8a6fc771e8ad => 185
	i64 16083117170034450818, ; 596: Avalonia.Controls => 0xdf32b5daa8e3a182 => 175
	i64 16154507427712707110, ; 597: System => 0xe03056ea4e39aa26 => 164
	i64 16219561732052121626, ; 598: System.Net.Security => 0xe1177575db7c781a => 73
	i64 16313030114241086891, ; 599: ja\Microsoft.CodeAnalysis.Scripting.resources => 0xe2638675719705ab => 324
	i64 16315482530584035869, ; 600: WindowsBase.dll => 0xe26c3ceb1e8d821d => 165
	i64 16324796876805858114, ; 601: SkiaSharp.dll => 0xe28d5444586b6342 => 221
	i64 16337011941688632206, ; 602: System.Security.Principal.Windows.dll => 0xe2b8b9cdc3aa638e => 127
	i64 16423015068819898779, ; 603: Xamarin.Kotlin.StdLib.Jdk8 => 0xe3ea453135e5c19b => 271
	i64 16454459195343277943, ; 604: System.Net.NetworkInformation => 0xe459fb756d988f77 => 68
	i64 16496768397145114574, ; 605: Mono.Android.Export.dll => 0xe4f04b741db987ce => 169
	i64 16555111656825353880, ; 606: Avalonia.Metal.dll => 0xe5bf9256d200f698 => 180
	i64 16648892297579399389, ; 607: CommunityToolkit.Mvvm => 0xe70cbf55c4f508dd => 197
	i64 16702652415771857902, ; 608: System.ValueTuple => 0xe7cbbde0b0e6d3ee => 151
	i64 16709499819875633724, ; 609: System.IO.Compression.ZipFile => 0xe7e4118e32240a3c => 45
	i64 16732243140885012032, ; 610: zh-Hans/Eremex.Avalonia.Charts.resources.dll => 0xe834de7c43eade40 => 277
	i64 16737807731308835127, ; 611: System.Runtime.Intrinsics => 0xe848a3736f733137 => 108
	i64 16758309481308491337, ; 612: System.IO.FileSystem.DriveInfo => 0xe89179af15740e49 => 48
	i64 16762783179241323229, ; 613: System.Reflection.TypeExtensions => 0xe8a15e7d0d927add => 96
	i64 16765015072123548030, ; 614: System.Diagnostics.TextWriterTraceListener.dll => 0xe8a94c621bfe717e => 31
	i64 16822611501064131242, ; 615: System.Data.DataSetExtensions => 0xe975ec07bb5412aa => 23
	i64 16833383113903931215, ; 616: mscorlib => 0xe99c30c1484d7f4f => 166
	i64 16856067890322379635, ; 617: System.Data.Common.dll => 0xe9ecc87060889373 => 22
	i64 16890310621557459193, ; 618: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 138
	i64 16933958494752847024, ; 619: System.Net.WebProxy.dll => 0xeb018187f0f3b4b0 => 78
	i64 16977952268158210142, ; 620: System.IO.Pipes.AccessControl => 0xeb9dcda2851b905e => 54
	i64 17008137082415910100, ; 621: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 10
	i64 17024911836938395553, ; 622: Xamarin.AndroidX.Annotation.Experimental.dll => 0xec44a31d250e5fa1 => 231
	i64 17062143951396181894, ; 623: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 16
	i64 17118171214553292978, ; 624: System.Threading.Channels => 0xed8ff6060fc420b2 => 139
	i64 17134447395689342536, ; 625: Avalonia.DesignerSupport => 0xedc9c91fcaae2648 => 176
	i64 17187273293601214786, ; 626: System.ComponentModel.Annotations.dll => 0xee8575ff9aa89142 => 13
	i64 17201328579425343169, ; 627: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 15
	i64 17202182880784296190, ; 628: System.Security.Cryptography.Encoding.dll => 0xeeba6e30627428fe => 122
	i64 17214520524539272569, ; 629: Avalonia.Themes.Simple.dll => 0xeee64335ebd59d79 => 195
	i64 17230721278011714856, ; 630: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 87
	i64 17234219099804750107, ; 631: System.Transactions.Local.dll => 0xef2c3ef5e11d511b => 149
	i64 17260702271250283638, ; 632: System.Data.Common => 0xef8a5543bba6bc76 => 22
	i64 17333249706306540043, ; 633: System.Diagnostics.Tracing.dll => 0xf08c12c5bb8b920b => 34
	i64 17338386382517543202, ; 634: System.Net.WebSockets.Client.dll => 0xf09e528d5c6da122 => 79
	i64 17375958953653056453, ; 635: Avalonia.MicroCom => 0xf123ce9b484233c5 => 181
	i64 17460814222308623673, ; 636: ru/Eremex.Avalonia.Controls3D.resources.dll => 0xf251460619cb4539 => 276
	i64 17470386307322966175, ; 637: System.Threading.Timer => 0xf27347c8d0d5709f => 147
	i64 17509662556995089465, ; 638: System.Net.WebSockets.dll => 0xf2fed1534ea67439 => 80
	i64 17509666368860808140, ; 639: Xamarin.AndroidX.Core.SplashScreen => 0xf2fed4cad38d63cc => 242
	i64 17565043795550961167, ; 640: pl\Microsoft.CodeAnalysis.Scripting.resources => 0xf3c39244b9fe7e0f => 326
	i64 17627500474728259406, ; 641: System.Globalization => 0xf4a176498a351f4e => 42
	i64 17685921127322830888, ; 642: System.Diagnostics.Debug.dll => 0xf571038fafa74828 => 26
	i64 17704177640604968747, ; 643: Xamarin.AndroidX.Loader => 0xf5b1dfc36cac272b => 256
	i64 17710060891934109755, ; 644: Xamarin.AndroidX.Lifecycle.ViewModel => 0xf5c6c68c9e45303b => 254
	i64 17712670374920797664, ; 645: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 107
	i64 17748157696115377834, ; 646: Avalonia.Diagnostics.dll => 0xf64e1f640e9286aa => 188
	i64 17777608984931640715, ; 647: System.Resources.Extensions => 0xf6b6c12e96a4a58b => 228
	i64 17777860260071588075, ; 648: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 110
	i64 17838668724098252521, ; 649: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 7
	i64 17877291088293713194, ; 650: zh-Hans/Microsoft.CodeAnalysis.CSharp.resources.dll => 0xf818e586e000d52a => 304
	i64 17891337867145587222, ; 651: Xamarin.Jetbrains.Annotations => 0xf84accff6fb52a16 => 267
	i64 17928294245072900555, ; 652: System.IO.Compression.FileSystem.dll => 0xf8ce18a0b24011cb => 44
	i64 17929003892414330385, ; 653: NAudio.dll => 0xf8d09e0c57f3b611 => 212
	i64 17992315986609351877, ; 654: System.Xml.XmlDocument.dll => 0xf9b18c0ffc6eacc5 => 161
	i64 18003570342426260574, ; 655: NAudio.Core => 0xf9d987d6e513345e => 214
	i64 18025913125965088385, ; 656: System.Threading => 0xfa28e87b91334681 => 148
	i64 18116111925905154859, ; 657: Xamarin.AndroidX.Arch.Core.Runtime => 0xfb695bd036cb632b => 236
	i64 18146411883821974900, ; 658: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 38
	i64 18146811631844267958, ; 659: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 15
	i64 18225059387460068507, ; 660: System.Threading.ThreadPool.dll => 0xfcec6af3cff4a49b => 146
	i64 18245806341561545090, ; 661: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 8
	i64 18260797123374478311, ; 662: Xamarin.AndroidX.Emoji2 => 0xfd6b623bde35f3e7 => 246
	i64 18307520486456877007, ; 663: fr\Microsoft.CodeAnalysis.CSharp.Scripting.resources => 0xfe1160e29172dfcf => 309
	i64 18318849532986632368, ; 664: System.Security.dll => 0xfe39a097c37fa8b0 => 130
	i64 18380184030268848184, ; 665: Xamarin.AndroidX.VersionedParcelable => 0xff1387fe3e7b7838 => 264
	i64 18421262789791132424, ; 666: zh-Hans/Eremex.Avalonia.Controls3D.resources.dll => 0xffa578e8439f2b08 => 279
	i64 18439108438687598470 ; 667: System.Reflection.Metadata.dll => 0xffe4df6e2ee1c786 => 94
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [668 x i32] [
	i32 204, ; 0
	i32 171, ; 1
	i32 226, ; 2
	i32 190, ; 3
	i32 58, ; 4
	i32 237, ; 5
	i32 151, ; 6
	i32 259, ; 7
	i32 238, ; 8
	i32 241, ; 9
	i32 330, ; 10
	i32 132, ; 11
	i32 224, ; 12
	i32 56, ; 13
	i32 227, ; 14
	i32 215, ; 15
	i32 95, ; 16
	i32 174, ; 17
	i32 209, ; 18
	i32 253, ; 19
	i32 129, ; 20
	i32 223, ; 21
	i32 207, ; 22
	i32 175, ; 23
	i32 145, ; 24
	i32 18, ; 25
	i32 245, ; 26
	i32 212, ; 27
	i32 150, ; 28
	i32 104, ; 29
	i32 293, ; 30
	i32 95, ; 31
	i32 178, ; 32
	i32 173, ; 33
	i32 217, ; 34
	i32 36, ; 35
	i32 223, ; 36
	i32 28, ; 37
	i32 235, ; 38
	i32 193, ; 39
	i32 318, ; 40
	i32 50, ; 41
	i32 115, ; 42
	i32 315, ; 43
	i32 70, ; 44
	i32 297, ; 45
	i32 65, ; 46
	i32 170, ; 47
	i32 145, ; 48
	i32 292, ; 49
	i32 310, ; 50
	i32 234, ; 51
	i32 255, ; 52
	i32 40, ; 53
	i32 89, ; 54
	i32 81, ; 55
	i32 302, ; 56
	i32 218, ; 57
	i32 66, ; 58
	i32 62, ; 59
	i32 86, ; 60
	i32 233, ; 61
	i32 106, ; 62
	i32 259, ; 63
	i32 102, ; 64
	i32 35, ; 65
	i32 230, ; 66
	i32 197, ; 67
	i32 201, ; 68
	i32 294, ; 69
	i32 209, ; 70
	i32 119, ; 71
	i32 254, ; 72
	i32 317, ; 73
	i32 142, ; 74
	i32 194, ; 75
	i32 141, ; 76
	i32 270, ; 77
	i32 53, ; 78
	i32 35, ; 79
	i32 141, ; 80
	i32 215, ; 81
	i32 216, ; 82
	i32 218, ; 83
	i32 222, ; 84
	i32 303, ; 85
	i32 245, ; 86
	i32 8, ; 87
	i32 14, ; 88
	i32 258, ; 89
	i32 51, ; 90
	i32 280, ; 91
	i32 251, ; 92
	i32 136, ; 93
	i32 101, ; 94
	i32 214, ; 95
	i32 213, ; 96
	i32 186, ; 97
	i32 263, ; 98
	i32 308, ; 99
	i32 116, ; 100
	i32 242, ; 101
	i32 229, ; 102
	i32 163, ; 103
	i32 166, ; 104
	i32 67, ; 105
	i32 80, ; 106
	i32 101, ; 107
	i32 260, ; 108
	i32 117, ; 109
	i32 180, ; 110
	i32 328, ; 111
	i32 330, ; 112
	i32 306, ; 113
	i32 283, ; 114
	i32 78, ; 115
	i32 184, ; 116
	i32 114, ; 117
	i32 121, ; 118
	i32 48, ; 119
	i32 228, ; 120
	i32 290, ; 121
	i32 128, ; 122
	i32 250, ; 123
	i32 231, ; 124
	i32 82, ; 125
	i32 110, ; 126
	i32 75, ; 127
	i32 273, ; 128
	i32 316, ; 129
	i32 53, ; 130
	i32 222, ; 131
	i32 261, ; 132
	i32 69, ; 133
	i32 205, ; 134
	i32 83, ; 135
	i32 172, ; 136
	i32 116, ; 137
	i32 156, ; 138
	i32 226, ; 139
	i32 167, ; 140
	i32 199, ; 141
	i32 32, ; 142
	i32 122, ; 143
	i32 72, ; 144
	i32 62, ; 145
	i32 292, ; 146
	i32 289, ; 147
	i32 161, ; 148
	i32 113, ; 149
	i32 211, ; 150
	i32 88, ; 151
	i32 188, ; 152
	i32 105, ; 153
	i32 18, ; 154
	i32 327, ; 155
	i32 146, ; 156
	i32 118, ; 157
	i32 58, ; 158
	i32 241, ; 159
	i32 17, ; 160
	i32 52, ; 161
	i32 190, ; 162
	i32 92, ; 163
	i32 55, ; 164
	i32 129, ; 165
	i32 152, ; 166
	i32 41, ; 167
	i32 182, ; 168
	i32 92, ; 169
	i32 316, ; 170
	i32 283, ; 171
	i32 264, ; 172
	i32 50, ; 173
	i32 187, ; 174
	i32 332, ; 175
	i32 162, ; 176
	i32 13, ; 177
	i32 252, ; 178
	i32 229, ; 179
	i32 36, ; 180
	i32 67, ; 181
	i32 109, ; 182
	i32 275, ; 183
	i32 99, ; 184
	i32 291, ; 185
	i32 99, ; 186
	i32 11, ; 187
	i32 302, ; 188
	i32 217, ; 189
	i32 321, ; 190
	i32 11, ; 191
	i32 322, ; 192
	i32 314, ; 193
	i32 198, ; 194
	i32 25, ; 195
	i32 128, ; 196
	i32 76, ; 197
	i32 109, ; 198
	i32 274, ; 199
	i32 225, ; 200
	i32 263, ; 201
	i32 106, ; 202
	i32 2, ; 203
	i32 26, ; 204
	i32 219, ; 205
	i32 157, ; 206
	i32 199, ; 207
	i32 21, ; 208
	i32 49, ; 209
	i32 192, ; 210
	i32 277, ; 211
	i32 43, ; 212
	i32 126, ; 213
	i32 232, ; 214
	i32 296, ; 215
	i32 59, ; 216
	i32 179, ; 217
	i32 119, ; 218
	i32 300, ; 219
	i32 239, ; 220
	i32 3, ; 221
	i32 38, ; 222
	i32 301, ; 223
	i32 124, ; 224
	i32 307, ; 225
	i32 325, ; 226
	i32 285, ; 227
	i32 137, ; 228
	i32 149, ; 229
	i32 85, ; 230
	i32 90, ; 231
	i32 298, ; 232
	i32 253, ; 233
	i32 183, ; 234
	i32 177, ; 235
	i32 220, ; 236
	i32 333, ; 237
	i32 220, ; 238
	i32 174, ; 239
	i32 313, ; 240
	i32 182, ; 241
	i32 295, ; 242
	i32 251, ; 243
	i32 244, ; 244
	i32 133, ; 245
	i32 221, ; 246
	i32 96, ; 247
	i32 3, ; 248
	i32 105, ; 249
	i32 33, ; 250
	i32 154, ; 251
	i32 158, ; 252
	i32 177, ; 253
	i32 191, ; 254
	i32 186, ; 255
	i32 155, ; 256
	i32 82, ; 257
	i32 281, ; 258
	i32 143, ; 259
	i32 87, ; 260
	i32 19, ; 261
	i32 249, ; 262
	i32 51, ; 263
	i32 298, ; 264
	i32 61, ; 265
	i32 54, ; 266
	i32 4, ; 267
	i32 97, ; 268
	i32 17, ; 269
	i32 301, ; 270
	i32 155, ; 271
	i32 84, ; 272
	i32 192, ; 273
	i32 189, ; 274
	i32 29, ; 275
	i32 227, ; 276
	i32 274, ; 277
	i32 45, ; 278
	i32 64, ; 279
	i32 66, ; 280
	i32 172, ; 281
	i32 1, ; 282
	i32 268, ; 283
	i32 47, ; 284
	i32 314, ; 285
	i32 24, ; 286
	i32 234, ; 287
	i32 294, ; 288
	i32 308, ; 289
	i32 286, ; 290
	i32 165, ; 291
	i32 108, ; 292
	i32 275, ; 293
	i32 12, ; 294
	i32 250, ; 295
	i32 63, ; 296
	i32 27, ; 297
	i32 23, ; 298
	i32 93, ; 299
	i32 168, ; 300
	i32 12, ; 301
	i32 272, ; 302
	i32 183, ; 303
	i32 29, ; 304
	i32 103, ; 305
	i32 185, ; 306
	i32 14, ; 307
	i32 181, ; 308
	i32 126, ; 309
	i32 325, ; 310
	i32 203, ; 311
	i32 91, ; 312
	i32 252, ; 313
	i32 299, ; 314
	i32 9, ; 315
	i32 290, ; 316
	i32 86, ; 317
	i32 293, ; 318
	i32 247, ; 319
	i32 71, ; 320
	i32 168, ; 321
	i32 329, ; 322
	i32 1, ; 323
	i32 5, ; 324
	i32 44, ; 325
	i32 27, ; 326
	i32 304, ; 327
	i32 204, ; 328
	i32 269, ; 329
	i32 306, ; 330
	i32 158, ; 331
	i32 257, ; 332
	i32 112, ; 333
	i32 194, ; 334
	i32 191, ; 335
	i32 121, ; 336
	i32 287, ; 337
	i32 295, ; 338
	i32 224, ; 339
	i32 279, ; 340
	i32 233, ; 341
	i32 307, ; 342
	i32 159, ; 343
	i32 131, ; 344
	i32 216, ; 345
	i32 57, ; 346
	i32 284, ; 347
	i32 138, ; 348
	i32 83, ; 349
	i32 30, ; 350
	i32 10, ; 351
	i32 278, ; 352
	i32 281, ; 353
	i32 329, ; 354
	i32 210, ; 355
	i32 171, ; 356
	i32 284, ; 357
	i32 239, ; 358
	i32 150, ; 359
	i32 94, ; 360
	i32 319, ; 361
	i32 311, ; 362
	i32 247, ; 363
	i32 60, ; 364
	i32 319, ; 365
	i32 157, ; 366
	i32 195, ; 367
	i32 64, ; 368
	i32 88, ; 369
	i32 79, ; 370
	i32 47, ; 371
	i32 143, ; 372
	i32 173, ; 373
	i32 270, ; 374
	i32 244, ; 375
	i32 74, ; 376
	i32 91, ; 377
	i32 178, ; 378
	i32 196, ; 379
	i32 213, ; 380
	i32 310, ; 381
	i32 267, ; 382
	i32 289, ; 383
	i32 135, ; 384
	i32 90, ; 385
	i32 261, ; 386
	i32 276, ; 387
	i32 273, ; 388
	i32 282, ; 389
	i32 240, ; 390
	i32 207, ; 391
	i32 286, ; 392
	i32 324, ; 393
	i32 112, ; 394
	i32 42, ; 395
	i32 159, ; 396
	i32 4, ; 397
	i32 103, ; 398
	i32 70, ; 399
	i32 60, ; 400
	i32 39, ; 401
	i32 235, ; 402
	i32 320, ; 403
	i32 153, ; 404
	i32 56, ; 405
	i32 34, ; 406
	i32 300, ; 407
	i32 232, ; 408
	i32 21, ; 409
	i32 163, ; 410
	i32 331, ; 411
	i32 265, ; 412
	i32 140, ; 413
	i32 225, ; 414
	i32 89, ; 415
	i32 238, ; 416
	i32 147, ; 417
	i32 243, ; 418
	i32 162, ; 419
	i32 312, ; 420
	i32 6, ; 421
	i32 169, ; 422
	i32 31, ; 423
	i32 107, ; 424
	i32 323, ; 425
	i32 265, ; 426
	i32 208, ; 427
	i32 230, ; 428
	i32 260, ; 429
	i32 167, ; 430
	i32 249, ; 431
	i32 323, ; 432
	i32 287, ; 433
	i32 140, ; 434
	i32 59, ; 435
	i32 144, ; 436
	i32 210, ; 437
	i32 278, ; 438
	i32 219, ; 439
	i32 81, ; 440
	i32 187, ; 441
	i32 74, ; 442
	i32 130, ; 443
	i32 320, ; 444
	i32 25, ; 445
	i32 7, ; 446
	i32 176, ; 447
	i32 93, ; 448
	i32 262, ; 449
	i32 137, ; 450
	i32 113, ; 451
	i32 9, ; 452
	i32 184, ; 453
	i32 104, ; 454
	i32 19, ; 455
	i32 248, ; 456
	i32 256, ; 457
	i32 333, ; 458
	i32 33, ; 459
	i32 201, ; 460
	i32 46, ; 461
	i32 30, ; 462
	i32 237, ; 463
	i32 57, ; 464
	i32 134, ; 465
	i32 114, ; 466
	i32 305, ; 467
	i32 271, ; 468
	i32 297, ; 469
	i32 55, ; 470
	i32 288, ; 471
	i32 6, ; 472
	i32 77, ; 473
	i32 208, ; 474
	i32 189, ; 475
	i32 303, ; 476
	i32 331, ; 477
	i32 111, ; 478
	i32 206, ; 479
	i32 246, ; 480
	i32 299, ; 481
	i32 102, ; 482
	i32 321, ; 483
	i32 305, ; 484
	i32 309, ; 485
	i32 170, ; 486
	i32 115, ; 487
	i32 262, ; 488
	i32 76, ; 489
	i32 266, ; 490
	i32 85, ; 491
	i32 268, ; 492
	i32 236, ; 493
	i32 211, ; 494
	i32 160, ; 495
	i32 2, ; 496
	i32 313, ; 497
	i32 0, ; 498
	i32 24, ; 499
	i32 205, ; 500
	i32 32, ; 501
	i32 117, ; 502
	i32 37, ; 503
	i32 16, ; 504
	i32 52, ; 505
	i32 269, ; 506
	i32 179, ; 507
	i32 202, ; 508
	i32 202, ; 509
	i32 20, ; 510
	i32 123, ; 511
	i32 154, ; 512
	i32 328, ; 513
	i32 248, ; 514
	i32 326, ; 515
	i32 131, ; 516
	i32 148, ; 517
	i32 327, ; 518
	i32 120, ; 519
	i32 28, ; 520
	i32 200, ; 521
	i32 132, ; 522
	i32 100, ; 523
	i32 280, ; 524
	i32 134, ; 525
	i32 203, ; 526
	i32 311, ; 527
	i32 153, ; 528
	i32 97, ; 529
	i32 125, ; 530
	i32 69, ; 531
	i32 72, ; 532
	i32 258, ; 533
	i32 136, ; 534
	i32 124, ; 535
	i32 71, ; 536
	i32 111, ; 537
	i32 152, ; 538
	i32 266, ; 539
	i32 118, ; 540
	i32 243, ; 541
	i32 332, ; 542
	i32 127, ; 543
	i32 206, ; 544
	i32 133, ; 545
	i32 200, ; 546
	i32 322, ; 547
	i32 77, ; 548
	i32 46, ; 549
	i32 73, ; 550
	i32 63, ; 551
	i32 317, ; 552
	i32 98, ; 553
	i32 84, ; 554
	i32 43, ; 555
	i32 61, ; 556
	i32 257, ; 557
	i32 285, ; 558
	i32 37, ; 559
	i32 40, ; 560
	i32 272, ; 561
	i32 160, ; 562
	i32 98, ; 563
	i32 296, ; 564
	i32 240, ; 565
	i32 282, ; 566
	i32 291, ; 567
	i32 315, ; 568
	i32 312, ; 569
	i32 135, ; 570
	i32 20, ; 571
	i32 65, ; 572
	i32 125, ; 573
	i32 75, ; 574
	i32 0, ; 575
	i32 255, ; 576
	i32 164, ; 577
	i32 193, ; 578
	i32 156, ; 579
	i32 5, ; 580
	i32 49, ; 581
	i32 144, ; 582
	i32 196, ; 583
	i32 198, ; 584
	i32 139, ; 585
	i32 100, ; 586
	i32 123, ; 587
	i32 120, ; 588
	i32 142, ; 589
	i32 39, ; 590
	i32 318, ; 591
	i32 68, ; 592
	i32 288, ; 593
	i32 41, ; 594
	i32 185, ; 595
	i32 175, ; 596
	i32 164, ; 597
	i32 73, ; 598
	i32 324, ; 599
	i32 165, ; 600
	i32 221, ; 601
	i32 127, ; 602
	i32 271, ; 603
	i32 68, ; 604
	i32 169, ; 605
	i32 180, ; 606
	i32 197, ; 607
	i32 151, ; 608
	i32 45, ; 609
	i32 277, ; 610
	i32 108, ; 611
	i32 48, ; 612
	i32 96, ; 613
	i32 31, ; 614
	i32 23, ; 615
	i32 166, ; 616
	i32 22, ; 617
	i32 138, ; 618
	i32 78, ; 619
	i32 54, ; 620
	i32 10, ; 621
	i32 231, ; 622
	i32 16, ; 623
	i32 139, ; 624
	i32 176, ; 625
	i32 13, ; 626
	i32 15, ; 627
	i32 122, ; 628
	i32 195, ; 629
	i32 87, ; 630
	i32 149, ; 631
	i32 22, ; 632
	i32 34, ; 633
	i32 79, ; 634
	i32 181, ; 635
	i32 276, ; 636
	i32 147, ; 637
	i32 80, ; 638
	i32 242, ; 639
	i32 326, ; 640
	i32 42, ; 641
	i32 26, ; 642
	i32 256, ; 643
	i32 254, ; 644
	i32 107, ; 645
	i32 188, ; 646
	i32 228, ; 647
	i32 110, ; 648
	i32 7, ; 649
	i32 304, ; 650
	i32 267, ; 651
	i32 44, ; 652
	i32 212, ; 653
	i32 161, ; 654
	i32 214, ; 655
	i32 148, ; 656
	i32 236, ; 657
	i32 38, ; 658
	i32 15, ; 659
	i32 146, ; 660
	i32 8, ; 661
	i32 246, ; 662
	i32 309, ; 663
	i32 130, ; 664
	i32 264, ; 665
	i32 279, ; 666
	i32 94 ; 667
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.4xx @ cdb777a0c306e3e0668f847433f82144d7ca745f"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
