import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { PageService } from 'src/app/services/page.service';
import { AuthService } from 'src/app/services/user/auth.service';
import { UserService } from 'src/app/services/user/user.service';

declare var $ : any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent {
  constructor(private formBuilder:FormBuilder,private pageService : PageService,public authService : AuthService,spinner:NgxSpinnerService,private router:Router,private alertifyService:AlertifyService){
    super(spinner);
  }

  private bannersInfo : any;
  public banners : bannersContract[] = [];
  public page : number = 1;
  private pageSize : number = 6;
  public totalPage : number = 0;


  searchForm : FormGroup = this.formBuilder.group({
    ara : ["",Validators.required], 
  });



  async ngOnInit(){
    await this.sayfaGuncelle(this.page,this.pageSize);
    $( ".navbar-left a" ).on( "click", ()=>{
      this.sayfaGuncelle(this.page,this.pageSize);
    });
  }

  async search(){
    if(this.searchForm.valid){
    this.page = 1;
    this.pageSize = 6;
    this.banners = [];
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.bannersInfo = await this.pageService.getSearchedPages(this.page,this.pageSize,this.searchForm.get("ara").value);
    this.totalPage = Math.ceil(this.bannersInfo.count / this.pageSize);
    if(this.totalPage == 0){
      this.alertifyService.message("Konu bulunamadı.",MessageType.warning,PositionType.topCenter);
      this.totalPage = 1;
    }
    var i = 0;
    for (const item of this.bannersInfo.pages) {
      this.banners[i] = new bannersContract("https://localhost:7251/api/page/"+item.bannerImageName,item.bannerImageName,item.bannerTitle,item.pageId,item.bannerImageName,item.bannerId);
      i++;
    }

    this.hideSpinner(SpinnerType.ballNewtonCradle);
  }
  }


  async sayfayaGir(pageTitle : string,pageId : string){
    this.router.navigate(["guide/"+pageTitle],{queryParams:{Id:pageId}});
  }

  async sayfayiSil(bannerId:string,pageId:string,event:any){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    var response : any = await this.pageService.deletePage(pageId,bannerId,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    if(response.succeeded){
      this.alertifyService.message("Sayfa başarı ile silindi.",MessageType.success,PositionType.bottomCenter);
      $(event.srcElement.parentElement.parentElement).fadeOut(500);
    }else{
      this.alertifyService.message("Sayfa silinemedi.",MessageType.warning,PositionType.bottomCenter);
    }
  }

  async sayfaDegis(ust:boolean,word?:string){
    if(this.page < this.totalPage && ust){
      this.page = this.page + 1;
      if(word){
        this.sayfaGuncelle(this.page,this.pageSize,word);
      }else{
        this.sayfaGuncelle(this.page,this.pageSize);
      }
      
    } else if(this.page > 1 && !ust){
      this.page = this.page - 1;
      if(word){
        this.sayfaGuncelle(this.page,this.pageSize,word);
      }else{
        this.sayfaGuncelle(this.page,this.pageSize);
      }
    }
  }



  async sayfaGuncelle(page:number,pageSize:number,word?:string){
    this.banners = [];
    this.showSpinner(SpinnerType.ballNewtonCradle);
    if(word){
      this.bannersInfo = await this.pageService.getSearchedPages(this.page,this.pageSize,word);
    }else{
      this.bannersInfo = await this.pageService.getAllPageInfo(page,pageSize);
    }
    
    this.totalPage = Math.ceil(this.bannersInfo.count / this.pageSize);
    var i = 0;
    for (const item of this.bannersInfo.pages) {
      this.banners[i] = new bannersContract("https://localhost:7251/api/page/"+item.bannerImageName,item.bannerImageName,item.bannerTitle,item.pageId,item.bannerImageName,item.bannerId);
      i++;
    }

    this.hideSpinner(SpinnerType.ballNewtonCradle);
  }
}

export class bannersContract{
  bannerImageNameLink : string;
  bannerImageAlt : string;
  bannerTitle : string;
  pageId : string;
  bannerImageName : string;
  pageTitle : string;
  bannerId : string;
  constructor(bannerNameLink:string,bannerAlt:string,bannerTitle:string,pageId:string,bannerImageName:string,bannerId:string){
    this.bannerImageNameLink = bannerNameLink;
    this.bannerImageAlt = bannerAlt;
    this.bannerTitle = bannerTitle;
    this.pageId = pageId;
    this.bannerImageName = bannerImageName;
    this.bannerId = bannerId;
    this.pageTitle = this.bannerTitle.toLowerCase().replaceAll(" ","-");
  }
 
}