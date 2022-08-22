# Bootcamp Graduation Project - Merge Of Ages

<h2>Merge Of Ages</h2>
The arena of Wulimdar is an antique place where historical battles took place of. The actual constructor of the building is not known. After it was discovered, all countries started a war with each other to take control of this mysterious place. Unfortunately, this war never ended and still going on the centuries since its discovery of it. Now, as a commander, it is up to you to decide whose in charge of this mysterious place. Prepare for the war that goes on for ages.

<h3>Oyun Hakkında</h3>

 Bir savaş oyunu.
 Oyunda satın alma sürecinde oyuncunun oyun içi coini bulunuyor. Bu coin ile yakın mesafe ya da uzak mesafe birlikler satın alabiliyoruz.
 Satın alınan karakterler bir particle effect ile ile spawn oluyor.
 Daha sonrasında iki yakın mesafe birliği birleştirdiğimizde(merge mekaniği) yakın birliğin bir üst leveli sahnemize spawn oluyor.
 Aynı durum uzak mesafe birlikler için de geçerli. Birlikleri bu şekilde birleştirerek sürekli olarak birliklerimizi üst levellera yükseltiyoruz.
 
 Ana ekranda en başta karşımızda da düşman birlikler olacak. Bu birliklerin seviyesi oyun leveli arttıkça artıyor olacak.
 Bütün karakterlerin kendine göre health, armor, attack değişkenleri olacak.
 Bu şekilde de savaş ekranı şekillenecek.
 
 Gelelim savaş sürecine,
 ana ekranda battle butonuna bastığımızda savaş sürecine geçiyoruz.
 Savaş durumunda ise her birlik en yakınındaki rakibini belirleyip ona kilitleniyor.
 Yani kitlendiği rakibe dönüp, ona doğru ilerleyip, ona saldıracak.
 Daha sonra rakibi ölünce kendine yeni rakip arayacak ve yeni rakibine kilitlenecek ta ki sahnede rakibi kalmayıncaya dek.
 
 Kazanma-kaybetme popup'ları ise,
 Eğer sahnede enemy kalmazsa biz kazanıyoruz ve kazandığımızı gösteren bir ui popup beliriyor, ui da kazandığımız parayı gösteriyor ve 
 bir üst levele geçmek için bir buton geliyor.
 Eğer sahnede bizim hiç askerimiz kalmazsa kaybediyoruz ve kaybettiğimizi gösteren bir ui oluşuyor ve aynı lelevi tekrar etmek için bir buton oluşuyor.
 
 <h3>Oyunun detaylı videosu</h3>

https://user-images.githubusercontent.com/67235777/185861352-3f278790-8ae8-4b10-a3cd-fa08ddb864a7.mp4

<h3> Oyunda Kullanılan Design Pattern'lar</h3>
 
 Oyunumuzda manager scriptlerine daha kolay erişebilmek adına Singleton Design Pattern'ı kullandık. Savaş durumununda dost ve düşman karakterlerin Idle, Lock, Attack vb. kontrollerini sağlamak için ise State Pattern'ı referans aldık.
 
 <h3>Grid'in Oluşturulması</h3>
 
 Oyunumuzda Grid sistemini kurarken generic bir yapı kurmak istedik. GridManager içerisinde tanımladığımız height ve width değişkenleri sayesinde "Height X Width" boyutlarında grid'ler oluşturabilmekteyiz. Biz oyunumuzda 6x6 grid kullandık.
 
 <h3>Merge Mekaniği</h3>
 
 Merge mekaniğinde işleri kolaylaştırabilmek adına gridi oluştururken her bir cell için bir Node class'ı oluşturduk. Bu node class'ı şu bilgileri içeriyor:
 - IsPlaceable : İlgili cell üzerinde herhangi bir asker bulunuyorsa o konuma başka askerin drag&drop etmemizi engelliyor.
 - CellPosition : İlgili cell'in transform değerlerinin tutulduğu field.
 - Tag : İlgili cell üzerindeki askerin Tag değerinin atandığı field. Eğer cell boş ise buradaki tag değerimiz de boş bir string değeri alıyor.
 
 Merge mekaniğinde ise aynı tag'e sahip iki objenin birini diğer objenin üzerine sürükleyip bıraktığımızda bir sonraki level objemiz oluşturulmuş oluyor. 
 
 
 <h3>Oyunda Kullanılan Modeller</h3>
 
 Modellerin detayı için: https://www.behance.net/gallery/150920351/Bootcamp-Graduation-Project-Merge-Of-Ages
 
 ![This is an image](https://cdn.discordapp.com/attachments/1002126905266425869/1011018478502817983/GameScene2.png)
 
 ![This is an image](https://cdn.discordapp.com/attachments/1002127222037024808/1010100799730368582/Render_Scene.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/CRender2-removebg-preview.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/CRender3-removebg-preview.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/Render_SCharacter4-removebg-preview.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/CRender-removebg-preview.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/Render_SCharacter2-removebg-preview.png)
 
 ![This is an image](https://raw.githubusercontent.com/harunesad/Bootcamp-Graduation-Project/main/Assets/Art/Images/Render_SCharacter8-removebg-preview.png)
