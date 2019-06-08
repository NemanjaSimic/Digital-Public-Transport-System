import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { NoviCenovnik } from 'src/app/models/novi-cenovnik';
import { CenovnikService } from 'src/app/services/cenovnik.service';
import { Stavka } from 'src/app/models/cenovnik';
import { Router } from '@angular/router';

@Component({
  selector: 'app-novi-cenovnik',
  templateUrl: './novi-cenovnik.component.html',
  styleUrls: ['./novi-cenovnik.component.css']
})
export class NoviCenovnikComponent implements OnInit {

  constructor(private router: Router,private fb: FormBuilder, private cenovnikService: CenovnikService) { }

  ngOnInit() {
  }
   
  napraviCenovnik(cenovnik: NoviCenovnik,form: NgForm){
    //  Object.entries(cenovnik).forEach(entry =>{
    //    let info = entry[0].split('_');
    //    this.stavke.push(new Stavka(info[0], info[1], entry[1]));
    //  })

      this.cenovnikService.napraviCenovnik(cenovnik).subscribe(
        (response) => {
          this.router.navigate(['/cenovnik']);   
        },
        (error) => {console.log(error);}
        );
  }
}
