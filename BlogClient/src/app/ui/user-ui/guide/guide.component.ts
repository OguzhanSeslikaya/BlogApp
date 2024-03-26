import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { PageService } from 'src/app/services/page.service';

@Component({
  selector: 'app-guide',
  templateUrl: './guide.component.html',
  styleUrls: ['./guide.component.scss']
})
export class GuideComponent extends BaseComponent {
  private page : any;
constructor(private route: ActivatedRoute,spinner :NgxSpinnerService,private pageService : PageService){
  super(spinner);
}



  async ngOnInit(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.page = await this.pageService.getPage(this.route.snapshot.queryParamMap.get("Id"));
    
    document.getElementById("page").innerHTML = this.page.innerHtml;
    this.hideSpinner(SpinnerType.ballNewtonCradle);
  }
}
