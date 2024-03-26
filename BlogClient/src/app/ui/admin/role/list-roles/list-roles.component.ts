import { AssignPermissionDialogComponent } from '../dialogs/assign-permission-dialog/assign-permission-dialog.component';
import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { RoleService } from 'src/app/services/admin/role.service';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';

declare var $ : any;

@Component({
  selector: 'app-list-roles',
  templateUrl: './list-roles.component.html',
  styleUrls: ['./list-roles.component.scss']
})
export class ListRolesComponent extends BaseComponent {

  constructor(private roleService : RoleService,spinner : NgxSpinnerService,public dialog: MatDialog,private alertifyService:AlertifyService){
    super(spinner);
  }
  public roles : any;
  private response : any;
  async ngOnInit(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.response = await this.roleService.getRoles(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    this.roles = this.response.roles;
  }

  async yeniRolListesi(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.response = await this.roleService.getRoles(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    this.roles = this.response.roles;
  }


  async openDialog(id:string) {

    const dialogRef = this.dialog.open(AssignPermissionDialogComponent,{data:id});
    dialogRef.afterClosed().subscribe(async result => {
          if(result){
          this.showSpinner(SpinnerType.ballNewtonCradle);
          var response : any = await this.roleService.assignRoleToEndpoints(id,result,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
          if(response.succeeded == true){
            this.alertifyService.message("Yetkiler başarı ile düzenlendi.",MessageType.success,PositionType.bottomCenter);
          }else
            this.alertifyService.message("Yetki düzenleme işlemi başarısız.",MessageType.error,PositionType.bottomCenter);
          }
    });
  }

}
