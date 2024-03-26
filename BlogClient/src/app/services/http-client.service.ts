import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable,Inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpClientService {

  constructor(private httpClient:HttpClient,@Inject("baseUrl") private url:string) {}

  linkOlustur(linkDevam:string):string{
    return `${this.url}${linkDevam}`;
  }

  get<T>(linkDevam:string):Observable<T>{
    var yeniUrl = this.linkOlustur(linkDevam);
    return this.httpClient.get<T>(yeniUrl);
  }

  post<T>(linkDevam:string,body:any):Observable<T>{
    var yeniUrl = this.linkOlustur(linkDevam);
    return this.httpClient.post<T>(yeniUrl,body);
  }

  delete<T>(linkDevam:string,body:any){
    var yeniUrl = this.linkOlustur(linkDevam);
    return this.httpClient.delete(yeniUrl,{body:body})
  }
}