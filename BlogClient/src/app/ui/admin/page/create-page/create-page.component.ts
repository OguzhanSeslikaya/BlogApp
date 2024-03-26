import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user/user.service';

declare var SUNEDITOR: any;
declare var $: any;

@Component({
  selector: 'app-create-page',
  templateUrl: './create-page.component.html',
  styleUrls: ['./create-page.component.scss']
})
export class CreatePageComponent extends BaseComponent{
  private formData : FormData;
  public editor: any;
  private banner : File;

  constructor(private userService: UserService,spinner:NgxSpinnerService,private formBuilder:FormBuilder,private alertifyService:AlertifyService) {
    super(spinner);
  }

  ngOnInit() {
    this.editor = SUNEDITOR.create((document.getElementById('editor')), {
      height: 300,
      buttonList: [
        [
          'undo', 'redo',
          'font', 'fontSize', 'formatBlock',
          'paragraphStyle', 'blockquote',
          'bold', 'underline', 'italic', 'strike', 'subscript', 'superscript',
          'fontColor', 'hiliteColor', 'textStyle',
          'outdent', 'indent',
          'align', 'horizontalRule', 'list', 'lineHeight',
          'table', 'link', 'image', 'video',
          'showBlocks', 'preview'
        ]
      ]
    });
  }

  pageForm : FormGroup = this.formBuilder.group(
    {
      title : ["",Validators.required],
      banner : [null,Validators.required]
    }
  );

  onFileChange(event){
    this.banner = event.target.files[0];
  }

  async click() {
    if(this.pageForm.valid){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    var html = document.createElement("div");
    html.classList.add("sun-editor-editable");
    html.style.background = "transparent";
    html.innerHTML = this.editor.getContents();

    var page = new File([html.innerHTML], 'user-page.html');

    var formData = new FormData();
    formData.append("page", page);
    formData.append("banner",this.banner);
    formData.append("title",this.pageForm.get("title").value);
    var response : any = await this.userService.createPage(formData,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
      if(response.basariDurum){
        this.alertifyService.message("Sayfa başarı ile oluşturuldu.",MessageType.success,PositionType.bottomCenter);
      }else{
        this.alertifyService.message("Sayfa oluşturulamadı.",MessageType.warning,PositionType.bottomCenter);
      }
    
    }else{
      this.alertifyService.message("Page Name ve Banner değerleri boş geçilemez.",MessageType.error,PositionType.bottomCenter)
    }
     
  }
}