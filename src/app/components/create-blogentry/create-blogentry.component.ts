import { Component, ViewChild, AfterViewInit} from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/models/User';

declare var tinymce: any; // Declare the tinymce global object

@Component({
  selector: 'app-create-blogentry',
  templateUrl: './create-blogentry.component.html',
  styleUrls: ['./create-blogentry.component.css'],
})
export class CreateBlogentryComponent implements AfterViewInit{
  @ViewChild('myEditor') myEditor :any;

  ngAfterViewInit() {
    // Ensure that the TinyMCE editor has been fully initialized
    if (this.myEditor && this.myEditor.editor) {
      console.log('TinyMCE editor initialized');
    }
  }
  logContent() {
    const content = tinymce.get('myEditor').getContent();
    console.log(content);
  }
  submit(){
    return 0;
  }

}
