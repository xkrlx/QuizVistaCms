import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.css']
})
export class BaseComponent implements OnInit {
  
  
  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    
  }
  
  ngOnInit(): void {
    const currentUrl = this.router.url;
    console.info(`base component: url=${currentUrl}`)

    if(!this.authService.isUserLoggedIn() && currentUrl != '/login' && currentUrl != '/register'){
      this.router.navigate(['/'])
    }


  }

}
