import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class FormsService {

  constructor(private httpClient:HttpClientService) { }
  async kayitOl(form:FormData,succerCallBack?:any,errorCallBack?:any){
  const data = lastValueFrom(this.httpClient.post("Users/signIn",form));
  await data.then(d => succerCallBack()).catch(e => errorCallBack());
  return data;
}

async girisYap(form:FormData,succerCallBack?:any,errorCallBack?:any){
  const data = lastValueFrom(this.httpClient.post("Users/logIn",form));
  await data.then(d => succerCallBack()).catch(e => errorCallBack());
  return data;
}
}