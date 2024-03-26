import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { firstValueFrom, lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PageService {
  constructor(private httpClient : HttpClientService) { }

  async getAllPageInfo(page : number,pageSize : number){
    var data = lastValueFrom(this.httpClient.get("page?page=" + page + "&pageSize=" + pageSize));
    await data.then().catch();
    return data;
  }

  async getSearchedPages(page : number,pageSize : number,search:string){
    var data = lastValueFrom(this.httpClient.post("page/getSearchedPages",{page:page,pageSize:pageSize,word:search}));
    await data.then().catch();
    return data;
  }

  async getPage(id:string){
    var data = lastValueFrom(this.httpClient.get("page/getPage/"+id));
    await data.then().catch();
    return data;
  }

  async deletePage(pageId:string,bannerId:string,successCallBack:Function,errorCallBack:Function){
    var data = lastValueFrom(this.httpClient.delete("page/delete",{pageId:pageId,bannerId:bannerId}));
    await data.then(()=>successCallBack()).catch(()=>errorCallBack());
    return data;
  }

}
