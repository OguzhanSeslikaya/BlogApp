import { Component } from '@angular/core';

declare var $ : any;

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  ngOnInit(): void {
    $( ".grid-container .menu button" ).on( "click", function() {
      $(".grid-container .menu #dropdown-"+this.id).slideToggle(200);
    });
  }
}
