import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrls: ['./update-contact.component.css']
})
export class UpdateContactComponent implements OnInit {
  contactForm !: FormGroup;
  public baseUrl: any;
  contactId !: any;
  getContactSubscription !: any;
  createContactSubscription !: any;
  constructor(private dialogRef: MatDialogRef<UpdateContactComponent>, private fb: FormBuilder, private httpClient: HttpClient,@Inject(MAT_DIALOG_DATA) public data: any ) 
  {
    this.contactId = data.key;
    this.contactForm = fb.group({
      id: [''],
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
   this.getContactById(this.contactId);
  }

  getContactById(id:any):void{
    this.getContactSubscription = this.httpClient.get(`${this.baseUrl}/api/getContact?id=${id}`).subscribe(response=>{
      console.log(response);
      this.contactForm.patchValue(response);
    });
  }

  close(status:any): void {
    if(status !== "cancel"){
    alert("Successfully Contact Updated!");
    }
    this.dialogRef.close(status);
  }

  ngOnDestroy():void{
    this.getContactSubscription?.unsubscribe();
    this.createContactSubscription?.unsubscribe();
  }

  onSubmit():void{
    if (this.contactForm.valid) {
      console.log(this.contactForm.value);
    }
    var body= this.contactForm.value;
    this.createContactSubscription = this.httpClient.put(`${this.baseUrl}/api/updateContact`, body).subscribe((response:any)=>{
      if(response.isSuccess){
       this.close("successfully updated");    
    }
    })
  }
}
