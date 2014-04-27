using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WP8jukebox.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _id;
       
        /// Sample ViewModel property; this property is used to identify the object.
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _realid;
        
        /// Sample ViewModel property; this property is used to identify the object.
        public string RealID
        {
            get
            {
                return _realid;
            }
            set
            {
                if (value != _realid)
                {
                    _realid = value;
                    NotifyPropertyChanged("RealID");
                }
            }
        }

        private string _lineOne;
        
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public string LineOne
        {
            get
            {
                return _lineOne;
            }
            set
            {
                if (value != _lineOne)
                {
                    _lineOne = value;
                    NotifyPropertyChanged("LineOne");
                }
            }
        }

        private string _lineTwo;
        
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        public string LineTwo
        {
            get
            {
                return _lineTwo;
            }
            set
            {
                if (value != _lineTwo)
                {
                    _lineTwo = value;
                    NotifyPropertyChanged("LineTwo");
                }
            }
        }

        private string _lineThree;
       
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        public string LineThree
        {
            get
            {
                return _lineThree;
            }
            set
            {
                if (value != _lineThree)
                {
                    _lineThree = value;
                    NotifyPropertyChanged("LineThree");
                }
            }
        }

        private int _lineFour;
       
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        public int LineFour
        {
            get
            {
                return _lineFour;
            }
            set
            {
                if (value != _lineFour)
                {
                    _lineFour = value;
                    NotifyPropertyChanged("LineFour");
                }
            }
        }

        private string _lineFive;
    
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public string LineFive
        {
            get
            {
                return _lineFive;
            }
            set
            {
                if (value != _lineFive)
                {
                    _lineFive = value;
                    NotifyPropertyChanged("LineFive");
                }
            }
        }

         private int _lineSix;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineSix
         {
             get
             {
                 return _lineSix;
             }
             set
             {
                 if (value != _lineSix)
                 {
                     _lineSix = value;
                     NotifyPropertyChanged("LineSix");
                 }
             }
         }

         private int _lineSeven;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineSeven
         {
             get
             {
                 return _lineSeven;
             }
             set
             {
                 if (value != _lineSeven)
                 {
                     _lineSeven = value;
                     NotifyPropertyChanged("LineSeven");
                 }
             }
         }

         private int _lineEight;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineEight
         {
             get
             {
                 return _lineEight;
             }
             set
             {
                 if (value != _lineEight)
                 {
                     _lineEight = value;
                     NotifyPropertyChanged("LineEight");
                 }
             }
         }

         private int _lineNine;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineNine
         {
             get
             {
                 return _lineNine;
             }
             set
             {
                 if (value != _lineNine)
                 {
                     _lineNine = value;
                     NotifyPropertyChanged("LineNine");
                 }
             }
         }

         private int _lineTen;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineTen
         {
             get
             {
                 return _lineTen;
             }
             set
             {
                 if (value != _lineTen)
                 {
                     _lineTen = value;
                     NotifyPropertyChanged("LineTen");
                 }
             }
         }

         private int _lineEleven;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineEleven
         {
             get
             {
                 return _lineEleven;
             }
             set
             {
                 if (value != _lineEleven)
                 {
                     _lineEleven = value;
                     NotifyPropertyChanged("LineEleven");
                 }
             }
         }

         private int _lineTwelve;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineTwelve
         {
             get
             {
                 return _lineTwelve;
             }
             set
             {
                 if (value != _lineTwelve)
                 {
                     _lineTwelve = value;
                     NotifyPropertyChanged("LineTwelve");
                 }
             }
         }

         private int _lineThirteen;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public int LineThirteen
         {
             get
             {
                 return _lineThirteen;
             }
             set
             {
                 if (value != _lineThirteen)
                 {
                     _lineThirteen = value;
                     NotifyPropertyChanged("LineThirteen");
                 }
             }
         }


         private string _venue;

         /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
         public string Venue
         {
             get
             {
                 return _venue;
             }
             set
             {
                 if (value != _venue)
                 {
                     _venue = value;
                     NotifyPropertyChanged("Venue");
                 }
             }
         }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}