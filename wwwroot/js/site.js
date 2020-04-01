var $elem = document.getElementsByClassName('homeTitle');

MotionUI.animateIn($elem, 'slide-in-left', function () {
    console.log('Transition finished!');
});