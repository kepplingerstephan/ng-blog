import { Component } from '@angular/core';


@Component({
  selector: 'app-create-blogentry',
  templateUrl: './create-blogentry.component.html',
  styleUrls: ['./create-blogentry.component.css'],
})
export class CreateBlogentryComponent {
  content : string= '';
  
  handleEvent($content:any){
    console.log($content.getContent());
  }

  logContent(){
    
    // var i = tinymce.getContent();
    console.log("");
  }
}
