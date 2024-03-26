import { lastValueFrom } from 'rxjs';
import { HttpClientService } from '../http-client.service';
import { Injectable } from '@angular/core';
import { Menu } from '../../contracts/menu';

@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private httpClientService: HttpClientService) { }
  async getAuthorizeDefinitionEndpoints(id:string,successCallBack : Function,errorCallBack : Function){
    const data = lastValueFrom(this.httpClientService.get("ApplicationServices/" + id));
    await data.then(s => successCallBack()).catch(e => {errorCallBack();});
    return data;
  }
}
