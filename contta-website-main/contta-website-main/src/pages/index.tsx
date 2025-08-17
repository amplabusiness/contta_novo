// Trigger deploy: no-op change
import Layout from '../components/Layout';
import Header from '../components/Header';
import LandingSection from '../components/Sections/LandingSection';
import ServicesSection from '../components/Sections/ServicesSection';
import OurGoalSection from '../components/Sections/OurGoalSection';
import ContactsSection from '../components/Sections/ContactsSection';
import Footer from '../components/Footer';
import VideoOverlay from '../components/VideoOverlay';

const Home = () => {
  return (
    <Layout>
      <Header />
      <VideoOverlay />
      <LandingSection />
      <OurGoalSection />
      <ServicesSection />
      <ContactsSection />
      <Footer />
    </Layout>
  );
};

export default Home;
