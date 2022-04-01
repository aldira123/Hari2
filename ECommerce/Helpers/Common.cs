namespace eCommerce.Helpers{
public static class  Common
{
    public static byte[] StreamToBytes(Stream streamContent) {
        MemoryStream ms = new MemoryStream();
        
        streamContent.CopyTo(ms);

        return ms.ToArray();
    }
    public static byte[] ToBytes(this Stream streamContent) {
        MemoryStream ms = new MemoryStream();
        
        streamContent.CopyTo(ms);
        
        return ms.ToArray();
    }    

    public static int ToInt(this string content){
        if(int.TryParse(content, out int result)){
            return result;
        }

        throw new InvalidOperationException("Anda belum login, silahkan login terlebih dahulu");
    }
}

}