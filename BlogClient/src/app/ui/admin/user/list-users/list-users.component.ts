import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user/user.service';
import { AssignRoleDialogComponent } from '../dialog/assign-role-dialog/assign-role-dialog.component';

@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.scss']
})
export class ListUsersComponent extends BaseComponent {
  constructor(private userService : UserService,spinner : NgxSpinnerService,public dialog: MatDialog,private alertifyService:AlertifyService){
    super(spinner);
  }
  public users : any;
  private response : any;
  async ngOnInit(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.response = await this.userService.getUsers(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    this.users = this.response.users;
  }

  async yeniUserListesi(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.response = await this.userService.getUsers(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    this.users = this.response.users;
  }


  async openDialog(id:string) {

    const dialogRef = this.dialog.open(AssignRoleDialogComponent);
    dialogRef.afterClosed().subscribe(async result => {
          if(result){
          this.showSpinner(SpinnerType.ballNewtonCradle);
          var response : any = await this.userService.assignUserToRole(id,result,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
          if(response.succeeded == true){
            this.showSpinner(SpinnerType.ballNewtonCradle);
            this.response = await this.userService.getUsers(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
            this.users = this.response.users;
            this.alertifyService.message("Rol başarı ile düzenlendi.",MessageType.success,PositionType.bottomCenter);
          }else
            this.alertifyService.message("Rol düzenleme işlemi başarısız.",MessageType.error,PositionType.bottomCenter);
          }
    });
  }
}
