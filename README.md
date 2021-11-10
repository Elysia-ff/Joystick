# Joystick
Joystick UI for Unity

### Preview
![image](https://user-images.githubusercontent.com/45890606/141072084-35312eb9-b90b-41e7-af84-02772615b18c.png)  
![joystick_preview](https://user-images.githubusercontent.com/45890606/141072767-be60a00a-bfde-4554-9476-a2aace179f50.gif)

---

### Min/Max Distance  
If the distance between center and `touch/mouse position` is out of `min/max distance`, will be ignored/clamped.  
![image](https://user-images.githubusercontent.com/45890606/141073200-0b9aeb0b-9c26-43fc-bc3b-3b1e0c76f846.png)  
![joystick_distance](https://user-images.githubusercontent.com/45890606/141073037-cd33452c-ec89-4c19-b517-fc87436dd9d6.gif)

---

### Unit Angle Count  
How many angles do you wish to use?  
In this gif, 4 directions. (0 means no snapping)  
![image](https://user-images.githubusercontent.com/45890606/141073463-936d63f8-3890-44c2-a50d-08991c009dcc.png)  
![joystick_4dir](https://user-images.githubusercontent.com/45890606/141073475-5f9bda6b-aa1c-4e89-8d30-c7201aa98234.gif)

---

### Min/Max Value Threshold  
If the distance is less than `minValueThreshold`, [Value](https://github.com/Elysia-ff/Joystick/blob/master/Assets/Joystick/Scripts/Joystick.cs#L27) returns 0.
 ([usage](https://github.com/Elysia-ff/Joystick/blob/master/Assets/JoystickExample/Player.cs#L11))  
With `maxValueThreshold`, it returns 1.  
If `minValueThreshold == maxValueThreshold`, it's always 1.  
![image](https://user-images.githubusercontent.com/45890606/141074120-8340e863-0d3a-46f4-b984-33601deb6102.png)  
![joystick_threshold](https://user-images.githubusercontent.com/45890606/141074318-76952940-0eef-4c37-9e72-90309c2ef769.gif)

