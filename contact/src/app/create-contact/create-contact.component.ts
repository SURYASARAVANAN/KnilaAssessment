import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Component({
  selector: 'app-create-contact',
  templateUrl: './create-contact.component.html',
  styleUrls: ['./create-contact.component.css']
})
export class CreateContactComponent implements OnInit {
  contactForm !: FormGroup;
  public baseUrl: any;
  createContactSubscription!: any;
  constructor(private dialogRef: MatDialogRef<CreateContactComponent>, private fb: FormBuilder, private httpClient: HttpClient) 
  { 
    this.contactForm = fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      postalCode: ['', [Validators.required, Validators.pattern('^[0-9]{5}$')]],
      address: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      city: ['', Validators.required],
      state: ['', Validators.required],
      country: ['', Validators.required]
    });
    this.baseUrl = environment.apiUrl;
  }

  ngOnInit(): void {
  }

  onSubmit():void{
    var body= this.contactForm.value;
    this.createContactSubscription = this.httpClient.post(`${this.baseUrl}/api/createContacts`, body).subscribe((response:any)=>{
      if(response.isSuccess){
       this.close("successfully created");    
    }
    })
  }

  close(status:any): void {
    if(status !== "cancel"){
    alert("Successfully Contact Created!");
    }
    this.dialogRef.close(status);
  }

  ngOnDestroy():void{
    this.createContactSubscription?.unsubscribe();
  }


}
