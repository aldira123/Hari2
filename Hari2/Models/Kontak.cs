namespace Hari2.Models;

public class Kontak
{
    public string Nama{get;set;}
    public string Nomor{get;set;}

    public Kontak(string nama, string nomor){
        Nama = nama;
        Nomor = nomor;
    }
}
