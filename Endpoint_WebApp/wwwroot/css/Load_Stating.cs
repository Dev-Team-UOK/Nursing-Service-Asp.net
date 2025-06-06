

/* load stating */
.loader - image {
position: absolute;
top: 0;
right: 0;
width: 38vw;
height: 100vh;
display: flex;
    align - items: center;
    justify - content: center;
overflow: hidden;
}
.loader - image img {
  width: 100 %;
height: 100 %;
object-fit: cover;
display: block;
}

.loader - bars {
position: relative;
    z - index: 2;
display: flex;
gap: 16px;

    /* خطوط دقیقاً وسط صفحه باشند */
    align - items: center;
    justify - content: center;
height: 100vh;
width: 100vw;
    pointer - events: none;
}

.bar {
  width: 12px;
height: 72px;
background: #ff7e29;
  border - radius: 6px;
transform - origin: center center;
animation: bar - grow 1.1s infinite;
}

.bar: nth - child(2) { animation - delay: 0.14s; }
.bar: nth - child(3) { animation - delay: 0.28s; }
.bar: nth - child(4) { animation - delay: 0.42s; }
.bar: nth - child(5) { animation - delay: 0.56s; }

@keyframes bar-grow {
  0%, 100% {
    transform: scaleY(1);
opacity: 1;
  }
  50 % {
transform: scaleY(0.4);
opacity: 0.5;
}
}
