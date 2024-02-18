import { Component, OnInit } from '@angular/core';
import { AttemptHttpService } from '../../../services/http/attempt-http.service';
import { UserResultBrief } from 'src/app/models/user-results/user-result-brief';

@Component({
  selector: 'app-brief',
  templateUrl: './brief.component.html',
  styleUrls: ['./brief.component.css']
})
export class BriefComponent {
  userResults!: UserResultBrief;


  constructor(private attemptHttpService: AttemptHttpService){ }

  ngOnInit(): void {
    
    this.attemptHttpService.getUserResults().subscribe(
      res=>{
        this.userResults = res.model

      },
      error=>{
        console.error(error)
      }
    );
  }


}
