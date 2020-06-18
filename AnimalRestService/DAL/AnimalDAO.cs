using AnimalRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimalRestService.DAL
{
    public class AnimalDAO
    {
        static String strCon = "Data Source=den1.mssql7.gear.host;Persist Security Info=True;User ID=animailsinthezoo;Password=Pp70_0c9N!Qc";
        private DataAnimalDataContext db = new DataAnimalDataContext(strCon);
        public List<Animal> SelectALL()
        {
            db.ObjectTrackingEnabled = false;
            List<Animal> animals = db.Animals.ToList<Animal>();
            return animals;
        }

        public bool InsertAnimal(Animal animals)
        {
            try
            {
                db.Animals.InsertOnSubmit(animals);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAnimals(Animal animals)
        {
            try
            {
                Animal data = db.Animals.SingleOrDefault(Animal => Animal.ID == animals.ID);
                if (data != null)
                {
                    data.KIND = animals.KIND;
                    data.HEIGHT = animals.HEIGHT;
                    data.SEX = animals.SEX;
                    data.AGE = animals.AGE;
                    data.WEIGHT = animals.WEIGHT;
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAnimals(Animal animals)
        {
            try
            {
                Animal animal = db.Animals.Single(u => u.ID == animals.ID);
                db.Animals.DeleteOnSubmit(animal);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Animal> SelectByName(String keyword)
        {
            db.ObjectTrackingEnabled = false;
            List<Animal> animals;

            animals = (from p in db.Animals
                       where p.KIND.Contains(keyword)
                       select p).ToList();
            return animals;

        }

        public Animal SelectByID(int id)
        {
            db.ObjectTrackingEnabled = false;
            List<Animal> animals;

            animals = (from p in db.Animals
                       where p.ID == id
                       select p).ToList();
            if (animals.Count > 0)
            {
                return animals[0];
            }
            else return null;
        }
    }
}